using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
	private Image staminaImage, staminaEmptyImage;
	private RectTransform staminaSize;
	private float chargeDelay, invisibleDelay;
	private float staminaMax, presentStamina;
	private float staminaMaxSize;
	private bool coroutineRun, organize;
	private Coroutine coroutine;

	[HideInInspector] public bool run;
	public float gaugeDecreaseSpeed, gaugeIncreaseSpeed;

	private void Awake()
	{
		staminaSize = transform.Find("StaminaBar").gameObject.GetComponent<RectTransform>();
		staminaEmptyImage = transform.Find("StaminaBackgrond").GetComponent<Image>();
		staminaImage = transform.Find("StaminaBar").GetComponent<Image>();
	}
	private void Start()
	{
		staminaSize.sizeDelta = GetComponent<RectTransform>().sizeDelta + new Vector2(-20, 0);

		staminaMaxSize = staminaSize.sizeDelta.x;

		staminaMax = 100;
		presentStamina = staminaMax;

		SetStaminaClarity(0);
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && presentStamina > 0 && !organize)
		{
			run = true;

			coroutineRun = false;
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}

			chargeDelay = 0;
			invisibleDelay = 0;

			SetStaminaClarity(1);

			Decrease();
			Scaling();
		}
		else
		{
			OrganizeTime();

			run = false;

			chargeDelay += Time.deltaTime;

			if (chargeDelay < 0.5f) return;

			Increase();
			Scaling();
		}
	}

	private void Decrease()
	{
		presentStamina -= Time.deltaTime * gaugeDecreaseSpeed;
		if (presentStamina <= 0)
		{
			presentStamina = 0;
		}
	}

	private void Increase()
	{
		presentStamina += Time.deltaTime * gaugeIncreaseSpeed;

		if (presentStamina >= staminaMax)
		{
			presentStamina = staminaMax;
		}

		Invisible();
	}

	private void Invisible()
	{
		if (staminaImage.color.a <= 0) return;

		invisibleDelay += Time.deltaTime;

		if (invisibleDelay >= 0.5f && !coroutineRun)
		{
			coroutineRun = true;
			coroutine = StartCoroutine(InvisibleEffect());
		}
	}

	private void Scaling()
	{
		float size = presentStamina / staminaMax * staminaMaxSize;

		staminaSize.sizeDelta = new Vector2(size, 40);
		staminaImage.fillAmount = presentStamina / staminaMax * staminaMaxSize;
	}

	private void SetStaminaClarity(float alpha)
	{
		staminaImage.color = new Color(staminaImage.color.r, staminaImage.color.g, staminaImage.color.b, alpha);
		staminaEmptyImage.color = new Color(staminaEmptyImage.color.r, staminaEmptyImage.color.g, staminaEmptyImage.color.b, alpha);
	}

	private void OrganizeTime()
	{
		//다시 찰 때 색변화
		if (presentStamina <= 0)
		{
			organize = true;
			staminaImage.color = new Color(1, 0, 0, staminaImage.color.a);
			StartCoroutine(ChangeColor());
		}
		else if (presentStamina >= 50)
		{
			organize = false;
			staminaImage.color = new Color(0, 0, 1, staminaImage.color.a);
		}
	}

	private IEnumerator ChangeColor()
	{
		float color;
		while (staminaImage.color.r > 0)
		{
			color = presentStamina / staminaMax * gaugeIncreaseSpeed / 6;
			staminaImage.color = new Color(1 - color, 0, color, staminaImage.color.a);
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator InvisibleEffect()
	{
		float alpha = staminaImage.color.a;
		float decreaseValue = 30 / 255f;

		while (staminaImage.color.a > 0 && staminaEmptyImage.color.a > 0 && !organize)
		{
			SetStaminaClarity(alpha);
			alpha -= decreaseValue;
			if (alpha < 0)
			{
				alpha = 0;
			}
			yield return null;
		}

		coroutineRun = false;
	}
}

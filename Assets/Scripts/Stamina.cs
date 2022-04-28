using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
	//public RectTransform staminaBar;
	//public RectTransform stamina;
	//public float staminaMax;
	//public float haveStamina;
	//public bool staminaBool = true;

	//void Start()
	//{
	//	staminaMax = 10f;
	//	haveStamina = staminaMax;
	//}

	//void Update()
	//{
	//	InputStamina();
	//	StBool();
	//}



	//void InputStamina()
	//{
	//	if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && staminaBool == true)
	//	{
	//		//staminaBar = new Color(1, 1, 1, 1);
	//		haveStamina -= 2f * Time.deltaTime;
	//		staminaBar.gameObject.SetActive(true);
	//		stamina.gameObject.SetActive(true);
	//		stamina.localScale = new Vector3(0.1f * haveStamina, 0.4f, 0);
	//	}
	//	else
	//	{
	//		if (haveStamina < 10)
	//			haveStamina += 1f * Time.deltaTime;
	//		staminaBar.gameObject.SetActive(false);
	//		stamina.gameObject.SetActive(false);

	//	}

	//	//if(staminaBar.color)
	//}

	//void StBool()
	//{
	//	if (haveStamina <= 0)
	//	{
	//		haveStamina = 0;
	//		staminaBool = false;
	//	}
	//	else if (haveStamina >= 5f)
	//	{
	//		staminaBool = true;
	//	}
	//}

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
		staminaEmptyImage = transform.Find("StaminaEmpty").GetComponent<Image>();
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

			DecreaseStamina();
			ScalingStaminaBar();
		}
		else
		{
			OrganizeTime();

			run = false;

			chargeDelay += Time.deltaTime;

			if (chargeDelay < 0.5f) return;

			IncreaseStamina();
			ScalingStaminaBar();
		}
	}

	private void DecreaseStamina()
	{
		presentStamina -= Time.deltaTime * gaugeDecreaseSpeed;
		if (presentStamina <= 0)
		{
			presentStamina = 0;
		}
	}

	private void IncreaseStamina()
	{
		presentStamina += Time.deltaTime * gaugeIncreaseSpeed;

		if (presentStamina >= staminaMax)
		{
			presentStamina = staminaMax;
		}

		InvisibleStamina();
	}

	private void InvisibleStamina()
	{
		if (staminaImage.color.a <= 0) return;

		invisibleDelay += Time.deltaTime;

		if (invisibleDelay >= 0.5f && !coroutineRun)
		{
			coroutineRun = true;
			coroutine = StartCoroutine(StaminaEffect());
		}
	}

	private void ScalingStaminaBar()
	{
		staminaSize.sizeDelta = new Vector2(presentStamina / staminaMax * staminaMaxSize, 40);
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
		}
		else if (presentStamina >= 50)
		{
			organize = false;
			staminaImage.color = new Color(0, 0, 1, staminaImage.color.a);
		}
	}

	private IEnumerator StaminaEffect()
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

using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public UnityEvent<GameStateEnum> ChangeGameState;
	[SerializeField] private ToggleGroup toggleGroup;

	public void ChangeToggle(bool value)
	{
		Debug.Log(value);
		if (toggleGroup.AnyTogglesOn())
		{
			var toggles = toggleGroup.ActiveToggles().FirstOrDefault(x => x.isOn);

			if (toggles.name.Equals("ToggleShover"))
			{
                ChangeGameState?.Invoke(GameStateEnum.Shovel);
            }
			else if (toggles.name.Equals("ToggleHay"))
			{
                ChangeGameState?.Invoke(GameStateEnum.Hay);
            }
		}
		else
			ChangeGameState?.Invoke(GameStateEnum.None);
	}
}

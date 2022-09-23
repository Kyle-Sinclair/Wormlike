
using UnityEngine;

public static class SecondaryAbilityLibrary {
	
	public delegate void SecondaryFire (CharacterController character);

	public enum SecondaryFireAbility { Launch, Blink }

	static SecondaryFire[] Abilities = { Launch, Blink };
	
	public static SecondaryFire GetSecondaryFireAbility (SecondaryFireAbility name) {
		return Abilities[(int)name];
	}

	static public void Launch(CharacterController character)
	{
		Debug.Log("Secondary fire");

		character.Move(Vector3.up * 25);
	}
	static public void Blink(CharacterController character)
	{
		
	}
}

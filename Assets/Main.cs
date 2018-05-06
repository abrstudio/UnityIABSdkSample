using System;
using System.Collections.Generic;
using AbrStudioSdk.Core;
using AbrStudioSdk.Iab;
using AbrStudioSdk.Iab.Data;
using UnityEngine;

public class Main : MonoBehaviour {
	
	// Your application's API key that you got from abrstudio.
	private const string ApiKey = "PUT_YOUR_API_KEY_HERE";
	
	// Your application's Sign key that you got from abrstudio.
	private const string SignKey = "PUT_YOUR_SIGN_KEY_HERE";
	
	/* IabPublicKey should be YOUR APPLICATION'S PUBLIC KEY
	 * (that you got from the CafeBazaar developer console).
	 * It's the *app-specific* public key.
	 *
	 * Instead of just storing the entire literal string here embedded in the
	 * program,  construct the key at runtime from pieces or
	 * use bit manipulation (for example, XOR with some other string) to hide
	 * the actual key.  The key itself is not secret information, but we don't
	 * want to make it easy for an attacker to replace the public key with one
	 * of their own and then fake messages from the server.
	 */
	private const string IabPublicKey = "";
	
	// Sample sku id
	private const string SampleSku = "test";

	public void Start() 
	{
		AndroidJNIHelper.debug = false;
		
		// Just for debug mode and testing
		AbrStudio.EnableDebug(); // Comment this line for production
		
		// First of all you have to initialize the AbrStudio sdk using your app's API & Sign key.
		AbrStudio.Initialize(ApiKey, SignKey);
		
		/*
		 * You have to call AbrStudioIab#Start function before doing anything else related to in-app-billing.
		 */
		AbrStudioIab.Start(IabPublicKey, successInit, failedInit);
	}
	
	private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, Screen.width - 15f, Screen.height - 15f));
        GUI.skin.button.fixedHeight = 50;
        GUI.skin.button.fontSize = 20;

	    if (Button("Purchase Test"))
	    {
		    // User clicked the "Purchase Test" button
	        
		    // Generating Guid as developer payload just for test.
		    // You have to generate and pass your own developer payload string to AbrStudioIab#Purchase function
		    string developerPayload = Guid.NewGuid().ToString();
            
		    // launching the purchase flow
		    // You will be notified of purchase result via successPurchase or failedPurchase actions.
		    AbrStudioIab.Purchase(SampleSku, developerPayload, successPurchase, failedPurchase);
	    }

        GUILayout.EndArea();
    }

    bool Button(string label)
    {
        GUILayout.Space(5);
        return GUILayout.Button(label);
    }

	void successInit()
	{
		// AbrStudioIab started successfully.
		Debug.Log("AbrStudioIab started successfully.");
		
		// Querying inventory for owned items, providing a list of skus.
		AbrStudioIab.QueryInventory(new[] {SampleSku}, successQueryInventory, failedQueryInventory);
	}

	void failedInit(int code, string message)
	{
		// AbrStudioIab failed to start.
		// You can't do anything related to in-app-billing.
		
		Debug.Log("AbrStudioIab failed to start. (code: " + code + ", message: " + message + ")");
	}

	void successQueryInventory(Inventory inv)
	{
		// Query inventory was successful.
		// You can find your owned purchases (not consumed) in inv object.
		
		Debug.Log("Query inventory was successful.");
		foreach (var token in inv.PurchaseMap.Keys)
		{
			Debug.Log("You own: " + token);
		}
	}

	void failedQueryInventory(int code, string message)
	{
		// Query inventory wasn't successful.
		
		Debug.Log("Failed query inventory. (code: " + code + ", message: " + message + ")");
	}

	void successPurchase(Purchase purchase)
	{
		// Purchase was successfull.
		// You can find purchase token and other details in purchase object.
		
		Debug.Log("Purchase was successful.");
		
		// Consuming purchase after successfull purchase
		// You can consume the purchase whenever you want.
		AbrStudioIab.Consume(purchase, successConsume, failedConsume);
//		AbrStudioIab.MultiConsume(new[] {purchase}, multiConsumeResult);
	}
	
	void failedPurchase(int code, string message)
	{
		// Purchase was not successfull.
		
		Debug.Log("Failed purchase. (code: " + code + ", message: " + message + ")");
	}

	void successConsume(Purchase purchase)
	{
		// Consuming the purchase was successfull.
		// successfully consumed, so you can apply the effects of the item in your application
		
		Debug.Log("Consume was successful.");
	}

	void failedConsume(int code, string message)
	{
		// Consuming the purchase failed, try again later.
		
		Debug.Log("Failed consume. (code: " + code + ", message: " + message + ")");
	}
	
	void multiConsumeResult(List<Purchase> purchases, List<CodeMessagePair> codeMessagePairs)
	{
		Debug.Log("Multi consume result.");
		Debug.Log("code: " + codeMessagePairs[0].Code);
		Debug.Log("message: " + codeMessagePairs[0].Message);
	}
	

}

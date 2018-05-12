# UnityIABSdkSample

## WHAT IS THIS SAMPLE?
This project is a sample for AbrStudio Unity IAB SDK.
It's a very simple Unity application, where you can buy test product.


## HOW TO RUN THIS SAMPLE?
This sample can't be run as-is. Here is what you should do:

1. First you have to contact us for publishing your game in Iran. Visit [AbrStudio Website][website] or send an Email to [info@abrstudio.ir](mailto:info@abrstudio.ir) for more information.

[website]: http://abrstudio.ir "AbrStudio Website"

2. Get your application's credentials (including API key, Sign key and CafeBazaar public key) from us.
3. Change the sample's package name to your package name.

## WHAT DOES THIS SAMPLE DO?
This project is a sample for AbrStudio Unity In-App-Billing SDK.

### INITIALIZATION
In order to use IAB SDK you have to first initialize the SDK. The following code shows how this sample initializes the SDK.


```csharp
public class Main : MonoBehaviour {
	// Your application's API key that you got from abrstudio.
	private const string ApiKey = "PUT_YOUR_API_KEY_HERE";
	
	// Your application's Sign key that you got from abrstudio.
	private const string SignKey = "PUT_YOUR_SIGN_KEY_HERE";

	public void Start() 
	{
		// ...
		// First of all you have to initialize the AbrStudio sdk using your app's API & Sign key.
		AbrStudio.Initialize(ApiKey, SignKey);
	}
	// ...
}
```

### IN-APP-BILLING SDK
These are the main steps:
1. Start billing service: 

    ```csharp
    public class Main : MonoBehaviour {
	
		private const string IabPublicKey = "";

		private const string SampleSku = "test";

		public void Start() 
		{
			// ...

			/*
			 * You have to call AbrStudioIab#Start function before doing anything else related to in-app-billing.
			 */
			AbrStudioIab.Start(IabPublicKey, successInit, failedInit);

			// ...
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
	}
    ```
2. Purchase product:
	```csharp
	public class Main : MonoBehaviour {
		private void OnGUI()
		{
			// ...
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
			
			// ...
    	}
		
		void successPurchase(Purchase purchase)
		{
			// Purchase was successfull.
			// You can find purchase token and other details in purchase object.
		
			Debug.Log("Purchase was successful.");
			
			// You can consume the purchase now or whenever you want.
		}
	
		void failedPurchase(int code, string message)
		{
			// Purchase was not successfull.

			Debug.Log("Failed purchase. (code: " + code + ", message: " + message + ")");
		}
	}
	```
3. Consume:
	```csharp
	AbrStudioIab.Consume(purchase, successConsume, failedConsume);
	
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
	```
    
For more information about AbrStudio IAB SDK and how to use it, please visit [AbrStudio Website][website].

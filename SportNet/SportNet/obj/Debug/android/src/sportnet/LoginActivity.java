package sportnet;


public class LoginActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.common.GooglePlayServicesClient.ConnectionCallbacks,
		com.google.android.gms.common.GooglePlayServicesClient.OnConnectionFailedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onActivityResult:(IILandroid/content/Intent;)V:GetOnActivityResult_IILandroid_content_Intent_Handler\n" +
			"n_onConnected:(Landroid/os/Bundle;)V:GetOnConnected_Landroid_os_Bundle_Handler:Android.Gms.Common.IGooglePlayServicesClientConnectionCallbacksInvoker, GooglePlayServicesLib\n" +
			"n_onDisconnected:()V:GetOnDisconnectedHandler:Android.Gms.Common.IGooglePlayServicesClientConnectionCallbacksInvoker, GooglePlayServicesLib\n" +
			"n_onConnectionFailed:(Lcom/google/android/gms/common/ConnectionResult;)V:GetOnConnectionFailed_Lcom_google_android_gms_common_ConnectionResult_Handler:Android.Gms.Common.IGooglePlayServicesClientOnConnectionFailedListenerInvoker, GooglePlayServicesLib\n" +
			"";
		mono.android.Runtime.register ("SportNet.LoginActivity, SportNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginActivity.class, __md_methods);
	}


	public LoginActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoginActivity.class)
			mono.android.TypeManager.Activate ("SportNet.LoginActivity, SportNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onActivityResult (int p0, int p1, android.content.Intent p2)
	{
		n_onActivityResult (p0, p1, p2);
	}

	private native void n_onActivityResult (int p0, int p1, android.content.Intent p2);


	public void onConnected (android.os.Bundle p0)
	{
		n_onConnected (p0);
	}

	private native void n_onConnected (android.os.Bundle p0);


	public void onDisconnected ()
	{
		n_onDisconnected ();
	}

	private native void n_onDisconnected ();


	public void onConnectionFailed (com.google.android.gms.common.ConnectionResult p0)
	{
		n_onConnectionFailed (p0);
	}

	private native void n_onConnectionFailed (com.google.android.gms.common.ConnectionResult p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

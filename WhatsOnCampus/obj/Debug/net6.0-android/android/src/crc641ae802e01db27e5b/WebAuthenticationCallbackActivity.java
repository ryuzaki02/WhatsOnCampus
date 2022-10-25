package crc641ae802e01db27e5b;


public class WebAuthenticationCallbackActivity
	extends crc6468b6408a11370c2f.WebAuthenticatorCallbackActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("WhatsOnCampus.WebAuthenticationCallbackActivity, WhatsOnCampus", WebAuthenticationCallbackActivity.class, __md_methods);
	}


	public WebAuthenticationCallbackActivity ()
	{
		super ();
		if (getClass () == WebAuthenticationCallbackActivity.class)
			mono.android.TypeManager.Activate ("WhatsOnCampus.WebAuthenticationCallbackActivity, WhatsOnCampus", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
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

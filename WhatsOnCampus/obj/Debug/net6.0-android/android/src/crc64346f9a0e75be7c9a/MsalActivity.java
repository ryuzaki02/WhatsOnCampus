package crc64346f9a0e75be7c9a;


public class MsalActivity
	extends crc648316b0a9aa8cfd61.BrowserTabActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("WhatsOnCampus.Platforms.Android.MsalActivity, WhatsOnCampus", MsalActivity.class, __md_methods);
	}


	public MsalActivity ()
	{
		super ();
		if (getClass () == MsalActivity.class)
			mono.android.TypeManager.Activate ("WhatsOnCampus.Platforms.Android.MsalActivity, WhatsOnCampus", "", this, new java.lang.Object[] {  });
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

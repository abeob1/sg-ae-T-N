package com.abeo.tia.noordin;

import android.content.Context;
import android.content.SharedPreferences;

import com.loopj.android.http.AsyncHttpClient;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.RequestParams;

public class RestService 
{
	
	
	
	//private static final String BASE_URL = "http://192.168.0.107/Service1.asmx/";
    //private static final String BASE_URL = "http://54.251.51.69:3878/SPAMobile.asmx/";
	private static String BASE_URL = "";
	
	//private static final String BASE_URL = "http://"+SettingsActivity.url+"/";
	private static AsyncHttpClient client = new AsyncHttpClient();

	/*public static void get(String url, RequestParams params,
			AsyncHttpResponseHandler responseHandler) 
	{
		client.setTimeout(60000);
		client.get(getAbsoluteUrl(url), params, responseHandler);
	}*/

	public static void post(String url, RequestParams params,
			AsyncHttpResponseHandler responseHandler) 
	{
		/*try 
		{
		   MySSLSocketFactory sf;
		   KeyStore trustStore = KeyStore.getInstance(KeyStore.getDefaultType());
		   trustStore.load(null, null);
		   sf = new MySSLSocketFactory(trustStore);
		   sf.setHostnameVerifier(MySSLSocketFactory.BROWSER_COMPATIBLE_HOSTNAME_VERIFIER);
		   client.setSSLSocketFactory(sf);   
		}
		catch (Exception e) 
		{
		   e.printStackTrace();
		}*/
		client.setTimeout(30000);
		client.post(getAbsoluteUrl(url), params, responseHandler);
	}

	private static String getAbsoluteUrl(String relativeUrl) {
		return BASE_URL + relativeUrl;
	}
	
	public static void setBurl(String relativeUrl) {
		 BASE_URL = relativeUrl;
	}
	
	public static String getBurl() {
		return BASE_URL;
	}
}

package com.abeo.tia.noordin;

import java.net.InetAddress;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;
import abeo.tia.noordin.R;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.util.Patterns;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class Settings extends Activity {

	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ValidateUser

	Button submit;
	EditText url;
	String BURL;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.activity_settings);
		
		url = (EditText) findViewById(R.id.editText_url);
		
		
		
		

		submit = (Button) findViewById(R.id.button_submit);
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		BURL = prefLoginReturn.getString("apiurl", "");
		url.setText(BURL);
		System.out.println(BURL);
		
		
		submit.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				String enteredurl = url.getText().toString();
				if(Patterns.WEB_URL.matcher(enteredurl).matches())
				{
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					SharedPreferences.Editor edit = prefLoginReturn.edit();
					edit.putString("apiurl", enteredurl);
					edit.commit();
					BURL = prefLoginReturn.getString("apiurl", "");
					RestService.setBurl(BURL);
					Intent i = new Intent(Settings.this, LoginActivity.class);
					startActivity(i);
				}
				else
				{
					Toast.makeText(getApplicationContext(), "Kindly Enter Valide Url", Toast.LENGTH_SHORT).show();
				}
				
				
				
			}
		});

	}
	
	  public boolean onTouchEvent(MotionEvent event) {
	        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
	                                                        INPUT_METHOD_SERVICE);
	        imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	        return true;
	    }

}


package com.abeo.tia.noordin;

import java.io.File;
import java.net.InetAddress;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;
import abeo.tia.noordin.R;
import abeo.tia.noordin.R.bool;
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

	Button submit,genbtn,serverbtn;
	EditText url,genurl,serverurl;
	String BURL,enteredurl,enteredgurl,enteredsurl;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.activity_settings);
		
		url = (EditText) findViewById(R.id.editText_url);
		genurl = (EditText) findViewById(R.id.Download_url);
		serverurl = (EditText) findViewById(R.id.Server_url);
		
		
		
		

		submit = (Button) findViewById(R.id.button_submit);
		genbtn = (Button) findViewById(R.id.localpathbtn);
		serverbtn = (Button) findViewById(R.id.uploadpathbtn);
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		BURL = prefLoginReturn.getString("apiurl", "");
		enteredgurl = prefLoginReturn.getString("GenPath", "");
		enteredsurl = prefLoginReturn.getString("UploadPath", "");
		url.setText(BURL);
		genurl.setText(enteredgurl);
		serverurl.setText(enteredsurl);
		System.out.println(BURL);
		
		
		submit.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				
				if(val())
				{
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					SharedPreferences.Editor edit = prefLoginReturn.edit();
					edit.putString("apiurl", enteredurl);
					edit.putString("GenPath", enteredgurl);
					edit.putString("UploadPath", enteredsurl);
					edit.commit();
					BURL = prefLoginReturn.getString("apiurl", "");
					RestService.setBurl(BURL);
					Intent i = new Intent(Settings.this, LoginActivity.class);
					startActivity(i);
				}
				
			}
		});
		
		genbtn.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				Intent intent = new Intent(Settings.this, DirectoryPicker.class);
				startActivityForResult(intent, DirectoryPicker.PICK_DIRECTORY1);							
			}
		});
		serverbtn.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				Intent intent = new Intent(Settings.this, DirectoryPicker.class);
				startActivityForResult(intent, DirectoryPicker.PICK_DIRECTORY2);							
			}
		});
		
		

	}
	
	
	public boolean val()
	{
		enteredurl = url.getText().toString();
		enteredgurl = genurl.getText().toString();
		enteredsurl = serverurl.getText().toString();
		if(!Patterns.WEB_URL.matcher(enteredurl).matches())
		{
			Toast.makeText(getApplicationContext(), "Kindly Enter Valide Url", Toast.LENGTH_SHORT).show();
			return false;
		}
		else if(enteredgurl.isEmpty() || enteredgurl.equals(null))
		{
			Toast.makeText(getApplicationContext(), "Kindly Select Generate Path", Toast.LENGTH_SHORT).show();
			return false;
		}
		else if(enteredsurl.isEmpty() || enteredsurl.equals(null))
		{
			Toast.makeText(getApplicationContext(), "Kindly Select Upload Path", Toast.LENGTH_SHORT).show();
			return false;
		}
		else
			return true;
	}
	
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if(requestCode == DirectoryPicker.PICK_DIRECTORY1 && resultCode == RESULT_OK) {
			Bundle extras = data.getExtras();
			String path = (String) extras.get(DirectoryPicker.CHOSEN_DIRECTORY);
			// do stuff with path
			//FILESAVELOCATION = path;
			genurl.setText(path);
			System.out.println(path);
		}
		if(requestCode == DirectoryPicker.PICK_DIRECTORY2 && resultCode == RESULT_OK) {
			Bundle extras = data.getExtras();
			String path = (String) extras.get(DirectoryPicker.CHOSEN_DIRECTORY);
			// do stuff with path
			//FILESAVELOCATION = path;
			serverurl.setText(path);
			System.out.println(path);
		}
		
		
	}
	
	
	
	  public boolean onTouchEvent(MotionEvent event) {
	        InputMethodManager imm = (InputMethodManager)getSystemService(Context.
	                                                        INPUT_METHOD_SERVICE);
	        imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
	        return true;
	    }

}


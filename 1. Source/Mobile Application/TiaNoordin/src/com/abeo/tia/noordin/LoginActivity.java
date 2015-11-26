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
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends Activity {

	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ValidateUser

	private final String METHOD_NAME = "SPA_ValidateUser";
	private String tb_tm_NewCasesPro = "", tb_tm_ClosedCasesPro = "", tb_lm_NewCasesPro = "", tb_lm_ClosedCasesPro = "",FirstName="";
	private String tb_tm_NewCases = "", tb_tm_ClosedCases = "", tb_lm_NewCases = "", tb_lm_ClosedCases = "";
	private String ys_tm_turnaround = "", ys_tm_totaloutput = "", ys_lm_turnaround = "", ys_lm_totaloutput = "";
	private String priority = "", action = "", open = "",URol;
	String status = "";
	String message = "",BURL,genurl,serverurl;
	ProgressDialog dialog = null;
	JSONArray arrayResponse;
	JSONObject jsonResponse;
	Button login,settings;

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.activity_login);
		
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		BURL = prefLoginReturn.getString("apiurl", "");
		genurl = prefLoginReturn.getString("GenPath", "");
		serverurl = prefLoginReturn.getString("UploadPath", "");
		
		if(BURL.isEmpty() || BURL.equals(null))
		{
			SharedPreferences.Editor edit = prefLoginReturn.edit();
			edit.putString("apiurl", "http://54.251.51.69:3878/spamobile.asmx/");
			edit.commit();
			BURL = prefLoginReturn.getString("apiurl", "");
			RestService.setBurl(BURL);
		}
		else
		{
			RestService.setBurl(BURL);
		}
		
		if(genurl.isEmpty() || genurl.equals(null) || serverurl.isEmpty() || serverurl.equals(null))
		{
			Toast.makeText(LoginActivity.this, "Kindly Config your Path Settings!", Toast.LENGTH_LONG).show();
			Intent i = new Intent(LoginActivity.this, Settings.class);
			startActivity(i);
		}
			
		
		
		

		login = (Button) findViewById(R.id.button_login);
		settings = (Button) findViewById(R.id.button_settings);
		
		settings.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				Intent i = new Intent(LoginActivity.this, Settings.class);
				startActivity(i);
			}
		});

		login.setOnClickListener(new View.OnClickListener() {

			public void onClick(View arg0) {
				//Toast.makeText(LoginActivity.this, "Button Clicked!", Toast.LENGTH_SHORT).show();
			    System.out.println("Login Method Invoke");
			    
			    //bypass login check 
			    //Intent buyIntent = new Intent(LoginActivity.this, DashBoardActivity.class);
				//startActivity(buyIntent);
			    
				// call WebService
				EditText userName = (EditText) findViewById(R.id.editText_user);
				EditText userPassword = (EditText) findViewById(R.id.editText_pswd);
				userName.setError(null);
				userPassword.setError(null);
			 
				if (userName.getText().toString().trim().equals(""))
				{
					userName.requestFocus();
					userName.setError("Username should not be blank");
					//userName.setSelected(true);
					userPassword.setError(null);
																				
				}
				
				else if (userPassword.getText().toString().trim().equals(""))
				{
					userPassword.requestFocus();
					userPassword.setError("Password should not be blank");
					//userPassword.setSelected(true);
					userName.setError(null);
				}
				else
				{
					userPassword.setError(null);
					userName.setError(null);
					  String sUserName = userName.getText().toString();
					  String sPassword = userPassword.getText().toString();
					  String sCategory = "SPA";
					// call WebService
					  if(isOnline())
						  userLogin(sUserName, sPassword, sCategory);
					  else
						  Toast.makeText(LoginActivity.this, "Kindly check your internet connectivity!", Toast.LENGTH_LONG).show();
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

	public void userLogin(String UserName,String Password,String Category  ) {
		
		try {
			// On login click save related data in shared preference
			dialog = ProgressDialog.show(LoginActivity.this, "", "Credentials Authentication...", true);
			

			JSONObject jsonObject = new JSONObject();
			jsonObject.put("sUserName", UserName);
			jsonObject.put("sPassword", Password);
			jsonObject.put("sCategory", Category);

			RequestParams param = new RequestParams();
			param.put("sJsonInput", jsonObject.toString());
			
			RestService.post(METHOD_NAME, param, new BaseJsonHttpResponseHandler<String>() {

				@Override
				public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
					// TODO Auto-generated method stub
					System.out.println(arg3);
					dialog.dismiss();
					Toast.makeText(getApplicationContext(), "Kindly Check Your Webservice Url", Toast.LENGTH_LONG).show();
				}

				@Override
				public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
					try {
						// Find Login Response
						status = jsonResponse.getString("Status").toString();
						message = jsonResponse.getString("Message").toString();

						// Find Response for DashBoard
						tb_tm_NewCasesPro = jsonResponse.getString("TB_TM_NewCases_Per").toString();
						tb_tm_ClosedCasesPro = jsonResponse.getString("TB_TM_ClosedCases_Per").toString();
						tb_lm_NewCasesPro = jsonResponse.getString("TB_LM_NewCases_Per").toString();
						tb_lm_ClosedCasesPro = jsonResponse.getString("TB_LM_ClosedCases_Per").toString();

						tb_tm_NewCases = jsonResponse.getString("TB_TM_NewCases").toString();
						tb_tm_ClosedCases = jsonResponse.getString("TB_TM_ClosedCases").toString();
						tb_lm_NewCases = jsonResponse.getString("TB_LM_NewCases").toString();
						tb_lm_ClosedCases = jsonResponse.getString("TB_LM_ClosedCases").toString();
						ys_tm_turnaround = jsonResponse.getString("YS_TM_Turnaround").toString();
						ys_tm_totaloutput = jsonResponse.getString("YS_TM_Totaloutput").toString();
						ys_lm_turnaround = jsonResponse.getString("YS_LM_Turnaround").toString();
						ys_lm_totaloutput = jsonResponse.getString("YS_LM_Totaloutput").toString();
						FirstName = jsonResponse.getString("FirstName").toString();
						priority = jsonResponse.getString("Priority").toString();
						action = jsonResponse.getString("Action").toString();
						open = jsonResponse.getString("Open").toString();
						URol = jsonResponse.getString("SubRole").toString();
						
						String sUserName = jsonResponse.getString("UserName").toString();
						String sPassword = jsonResponse.getString("Password").toString();
						String sCategory = jsonResponse.getString("Category").toString();
						String sUserrole = jsonResponse.getString("SubRole").toString();
						//String sUserrole = jsonResponse.getString("SubRole").toString();
						
						
						
						SharedPreferences prefLogin = getSharedPreferences("LoginData", Context.MODE_PRIVATE);

						// We need an editor object to make changes
						SharedPreferences.Editor edit = prefLogin.edit();

						// Set/Store data
						edit.putString("sUserName", sUserName);
						edit.putString("sPassword", sPassword);
						edit.putString("sCategory", sCategory);
						edit.putString("sUserRole", sUserrole);
						edit.putString("CaseNo", "");						
						edit.putString("FIRSETNAME", FirstName);

						// Commit the changes
						edit.commit();

						// Find the SharedPreferences value
						SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
						System.out.println("LOGIN DATA");
						String user_name = prefLoginReturn.getString("sUserName", "");
						System.out.println(user_name);
						String Pswd = prefLoginReturn.getString("sPassword", "");
						System.out.println(Pswd);
						String catg = prefLoginReturn.getString("sCategory", "");
						System.out.println(catg);
						String sUserRole = prefLoginReturn.getString("sUserRole", "");
						System.out.println(sUserRole);
						
						
						
						

					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
						dialog.dismiss();
					}
					if (status.equals("true")) {
						Toast.makeText(getApplicationContext(), message, Toast.LENGTH_SHORT).show();
						Intent i = new Intent(LoginActivity.this, DashBoardActivity.class);

						// Find Progress Bar response for TB THIS MONTH & LAST
						// MONTH
						i.putExtra("TB_TM_NewCasesPro", tb_tm_NewCasesPro);
						System.out.println(tb_tm_NewCasesPro);
						i.putExtra("TB_TM_ClosedCasesPro", tb_tm_ClosedCasesPro);
						System.out.println(tb_tm_ClosedCasesPro);
						i.putExtra("TB_LM_NewCasesPro", tb_lm_NewCasesPro);
						System.out.println(tb_lm_NewCasesPro);
						i.putExtra("TB_LM_ClosedCasesPro", tb_lm_ClosedCasesPro);
						System.out.println(tb_lm_ClosedCasesPro);
						i.putExtra("FirstName", FirstName);
						System.out.println(FirstName);
						

						// Find response for TB THIS MONTH & LAST MONTH
						i.putExtra("TB_TM_NewCases", tb_tm_NewCases);
						System.out.println(tb_tm_NewCases);
						i.putExtra("TB_TM_ClosedCases", tb_tm_ClosedCases);
						System.out.println(tb_tm_ClosedCases);
						i.putExtra("TB_LM_NewCases", tb_lm_NewCases);
						System.out.println(tb_lm_NewCases);
						i.putExtra("TB_LM_ClosedCases", tb_lm_ClosedCases);
						System.out.println(tb_lm_ClosedCases);

						// Find response for YS THIS MONTH & LAST MONTH
						i.putExtra("YS_TM_Turnaround", ys_tm_turnaround);
						System.out.println(ys_tm_turnaround);
						i.putExtra("YS_TM_TOTALOUTPUT", ys_tm_totaloutput);
						System.out.println(ys_tm_totaloutput);
						i.putExtra("YS_LM_TURNAROUND", ys_lm_turnaround);
						System.out.println(ys_lm_turnaround);
						i.putExtra("YS_LM_TOTALOUTPUT", ys_lm_totaloutput);
						System.out.println(ys_lm_totaloutput);

						// Find response for PRIORITY, ACTION & OPEN
						i.putExtra("PRIORITY", priority);
						System.out.println(priority);
						i.putExtra("ACTION", action);
						System.out.println(action);
						i.putExtra("OPEN", open);
						System.out.println(open);
						startActivity(i);

					} else {
						Toast.makeText(getApplicationContext(), message, Toast.LENGTH_SHORT).show();
						// Progress dismiss
						dialog.dismiss();
						return;
					}
					// Progress dismiss
					dialog.dismiss();
					System.out.println(arg2);
				}

				@Override
				protected String parseResponse(String arg0, boolean arg1) throws Throwable {
					System.out.println(arg0);
					arrayResponse = new JSONArray(arg0);
					jsonResponse = arrayResponse.getJSONObject(0);
					return null;
				}
			});

		} catch (Exception e) {
			System.out.println(e.toString());
		}
	}

	 public boolean isOnline() {
	        boolean connected = false;
			try {
	            ConnectivityManager connectivityManager = (ConnectivityManager) this
	                        .getSystemService(Context.CONNECTIVITY_SERVICE);

	        NetworkInfo networkInfo = connectivityManager.getActiveNetworkInfo();
	        connected = networkInfo != null && networkInfo.isAvailable() &&
	                networkInfo.isConnected();
	        return connected;


	        } catch (Exception e) {
	            System.out.println("CheckConnectivity Exception: " + e.getMessage());
	           // Log.v("connectivity", e.toString());
	        }
	        return connected;
	    }

}


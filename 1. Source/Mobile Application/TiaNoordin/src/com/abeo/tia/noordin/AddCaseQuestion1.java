package com.abeo.tia.noordin;



import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

@SuppressLint("Recycle")
public class AddCaseQuestion1 extends BaseActivity {
	Button sell, buy, btnconfirm,btnntapplicable,btnactapp,walkin;
	private RadioGroup RadioGroup01,RadioGroup02,RadioGroup03,RadioGroup04,RadioGroup05;
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	//String qryGroup3 = "", vNDR_RP_FIRM_SELL = "", vNDR_RP_LWYR_SELL = "", qryGroup4 = "", vNDR_RP_FIRM_BUY = "",
		//	vNDR_RP_LWYR_BUY = "";
	
	// Find Web Service Message
		String messageDisplay = "", StatusResult = "";

		String userName = "", password = "", category = "", qryGroup3 = "", vNDR_RP_FIRM_SELL = "", vNDR_RP_LWYR_SELL = "",
				qryGroup4 = "", vNDR_RP_FIRM_BUY = "", vNDR_RP_LWYR_BUY = "", qryGroup6 = "", qryGroup5 = "",
				qryGroup7 = "", qryGroup8 = "", qryGroup9 = "", qryGroup10 = "", qryGroup11 = "",qryGroup17="",qryGroup21="";

	
	// Find Json Array
		JSONArray arrayResponse = null;
		JSONObject jsonResponse = null;
		
		// Find Add Question on web method
		private final String METHOD_ADD_QUESTION = "SPA_AddCase_Questions";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_question1);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);
		
		// Find the SharedPreferences Firstname
		SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
		String FirName = FirstName.getString("FIRSETNAME", "");
		TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
		welcome.setText("Welcome "+FirName);

		// Find button by Id
		sell = (Button) findViewById(R.id.btnsell);
		buy = (Button) findViewById(R.id.btnbuy);
		btnconfirm = (Button) findViewById(R.id.btn_confirm);
		walkin = (Button) findViewById(R.id.button_AddCaseQuestion1Walkin);
		

		sell.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {				
			    Button r2=(Button)findViewById(R.id.btnactapp);
			    r2.setEnabled(true);
			}
			
		});
		
		buy.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {				
			    Button r2=(Button)findViewById(R.id.btnactapp);
			    r2.setEnabled(false);
			}
			
		});
		
		walkin.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
		Intent i = new Intent(AddCaseQuestion1.this, WalkInActivity.class);
		startActivity(i);
			}
		});
		
		btnconfirm.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion1.this, "Confirm", Toast.LENGTH_SHORT).show();
				/*shortcut remove once done 
				Intent qIntent = new Intent(AddCaseQuestion1.this, AddCaseStep3of4.class);
				Toast.makeText(AddCaseQuestion1.this, messageDisplay, Toast.LENGTH_SHORT).show();
				startActivity(qIntent);*/

				
				if(validate())
				{
					int q1 = RadioGroup01.getCheckedRadioButtonId();
					RadioButton RG1 = (RadioButton) findViewById(q1);
					
						if(RG1.getText().equals("SELL"))
						{System.out.println("R1"+RG1.getText());
							qryGroup3= "Y";
							vNDR_RP_FIRM_SELL="Y";
							vNDR_RP_LWYR_SELL="Y";						
						}
						if(RG1.getText().equals("BUY"))
						{
							qryGroup3="Y";
							vNDR_RP_FIRM_SELL="Y";
							vNDR_RP_LWYR_SELL="Y";						
						}
						
					int q2 = RadioGroup02.getCheckedRadioButtonId();
					RadioButton RG2 = (RadioButton) findViewById(q2);
					
						if(RG2.getText().equals("SUB-SALE"))
						{System.out.println("R2"+RG2.getText());
							qryGroup6= "Y";					
						}
						if(RG2.getText().equals("DEVELOPER"))
						{
							qryGroup5="Y";						
						}
						
					int q3 = RadioGroup03.getCheckedRadioButtonId();
					RadioButton RG3 = (RadioButton) findViewById(q3);
					System.out.println("R3"+RG3.getText());
						if(RG3.getText().equals("YES"))
						{
							qryGroup17= "Y";					
						}
						if(RG3.getText().equals("NO"))
						{
							qryGroup21= "Y";					
						}
					int q4 = RadioGroup04.getCheckedRadioButtonId();
					RadioButton RG4 = (RadioButton) findViewById(q4);
					System.out.println("R4"+RG4.getText());
						if(RG4.getText().equals("COMMERCIAL"))
						{
							qryGroup7= "Y";					
						}
						if(RG4.getText().equals("LANDED"))
						{
							qryGroup8= "Y";					
						}
						if(RG4.getText().equals("APARTMENT / CONDO"))
						{
							qryGroup9= "Y";					
						}
						
					int q5 = RadioGroup05.getCheckedRadioButtonId();
					RadioButton RG5 = (RadioButton) findViewById(q5);
					System.out.println("R5"+RG5.getText());
						if(RG5.getText().equals("UNDER CONSTRUCTION"))
						{
							qryGroup10= "Y";					
						}
						if(RG5.getText().equals("COMPLETED"))
						{
							qryGroup11= "Y";					
						}
						
						
						
					//Toast.makeText(AddCaseQuestion1.this, selectedId, Toast.LENGTH_SHORT).show();
					
					// Find the SharedPreferences pass Login value
					SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
					System.out.println("LOGIN DATA");
					userName = prefLoginReturn.getString("sUserName", "");
					System.out.println(userName);
					password = prefLoginReturn.getString("sPassword", "");
					System.out.println(password);
					category = prefLoginReturn.getString("sCategory", "");
					System.out.println(category);


					try {

						// Passing answer in JSON format for question 1 to 4

						/*
						 * { "UserName": "LA001", "Password": "1234", "Category": "SPA",
						 * "QryGroup3": "Y", "VNDR_RP_FIRM_SELL": "Y", "VNDR_RP_LWYR_SELL":
						 * "Y", "QryGroup4": "", "VNDR_RP_FIRM_BUY": "", "VNDR_RP_LWYR_BUY":
						 * "", "QryGroup6": "Y", "QryGroup5": "", "QryGroup7": "Y",
						 * "QryGroup8": "", "QryGroup9": "", "QryGroup10": "", "QryGroup11":
						 * "Y" }
						 */
						JSONObject jsonObject = new JSONObject();
						jsonObject.put("UserName", userName);
						jsonObject.put("Password", password);
						jsonObject.put("Category", category);
						jsonObject.put("QryGroup3", qryGroup3);
						jsonObject.put("VNDR_RP_FIRM_SELL", vNDR_RP_FIRM_SELL);
						jsonObject.put("VNDR_RP_LWYR_SELL", vNDR_RP_LWYR_SELL);
						jsonObject.put("QryGroup4", qryGroup4);
						jsonObject.put("VNDR_RP_FIRM_BUY", vNDR_RP_FIRM_BUY);
						jsonObject.put("VNDR_RP_LWYR_BUY", vNDR_RP_LWYR_BUY);
						jsonObject.put("QryGroup6", qryGroup6);
						jsonObject.put("QryGroup5", qryGroup5);
						jsonObject.put("QryGroup7", qryGroup7);
						jsonObject.put("QryGroup8", qryGroup8);
						jsonObject.put("QryGroup9", qryGroup9);
						jsonObject.put("QryGroup10", qryGroup10);
						jsonObject.put("QryGroup11", qryGroup11);
						jsonObject.put("QryGroup17", qryGroup17);
						jsonObject.put("qryGroup21", qryGroup21);

						RequestParams params = new RequestParams();
						params.put("sJsonInput", jsonObject.toString());
						System.out.println(params);

						RestService.post(METHOD_ADD_QUESTION, params, new BaseJsonHttpResponseHandler<String>() {

							@Override
							public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
								System.out.println("Add Question OnFailure");
								System.out.println(arg3);

							}

							@Override
							public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
								System.out.println("Add Question OnSuccess");
								// Find status Response
								try {
									StatusResult = jsonResponse.getString("Result").toString();
									messageDisplay = jsonResponse.getString("DisplayMessage").toString();
									
									//store Card Code
									SharedPreferences prefLogin = getSharedPreferences("LoginData", Context.MODE_PRIVATE);									
									SharedPreferences.Editor edit = prefLogin.edit();
									edit.putString("CardCode", messageDisplay);									
									edit.commit();

									if (StatusResult.equals("SUCCESS") && !qryGroup5.equals("Y")) {
										Intent qIntent = new Intent(AddCaseQuestion1.this, AddCaseStep1of4.class);
										Toast.makeText(AddCaseQuestion1.this, messageDisplay, Toast.LENGTH_SHORT).show();
										startActivity(qIntent);

									}
									else if (StatusResult.equals("SUCCESS") && qryGroup5.equals("Y")) {
										
										Intent qIntent = new Intent(AddCaseQuestion1.this, AddCaseStep2of4.class);
										qIntent.putExtra("jsonArray", "[]");
										
										
										
										Toast.makeText(AddCaseQuestion1.this, messageDisplay, Toast.LENGTH_SHORT).show();
										startActivity(qIntent);

									}
									else {
										Toast.makeText(AddCaseQuestion1.this, messageDisplay, Toast.LENGTH_SHORT).show();

									}
								} catch (JSONException e) {
									// TODO Auto-generated catch block
									e.printStackTrace();
								}

								System.out.println(arg2);
							}

							@Override
							protected String parseResponse(String arg0, boolean arg1) throws Throwable {
								System.out.println("Add Question OnParseResponse");
								// Get Json response
								arrayResponse = new JSONArray(arg0);
								jsonResponse = arrayResponse.getJSONObject(0);

								System.out.println(arg0);
								return null;
							}
						});

					} catch (JSONException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
						
						
				}
				
				
				
				//Intent buyIntent = new Intent(AddCaseQuestion1.this, AddCaseStep1of4.class);
				//startActivity(buyIntent);

			}

		});
	}
	
	public boolean validate()
	{
		RadioGroup01 = (RadioGroup) findViewById(R.id.RadioGroup01);
		RadioGroup02 = (RadioGroup) findViewById(R.id.RadioGroup02);
		RadioGroup03 = (RadioGroup) findViewById(R.id.RadioGroup03);
		RadioGroup04 = (RadioGroup) findViewById(R.id.RadioGroup04);
		RadioGroup05 = (RadioGroup) findViewById(R.id.RadioGroup05);
		
		
		int q1 = RadioGroup01.getCheckedRadioButtonId();
		System.out.println(q1);
		if(q1==-1)
		{
			Toast.makeText(AddCaseQuestion1.this, "Select Answer for Question 1", Toast.LENGTH_SHORT).show();
			return false;
		}
		
		int q2 = RadioGroup02.getCheckedRadioButtonId();
		System.out.println(q2);
		if(q2==-1)
		{
			Toast.makeText(AddCaseQuestion1.this, "Select Answer for Question 2", Toast.LENGTH_SHORT).show();
			return false;
		}
		
		int q3 = RadioGroup03.getCheckedRadioButtonId();
		System.out.println(q3);
		if(q3==-1)
		{
			Toast.makeText(AddCaseQuestion1.this, "Select Answer for Question 3", Toast.LENGTH_SHORT).show();
			return false;
		}
		
		int q4 = RadioGroup04.getCheckedRadioButtonId();
		System.out.println(q4);
		if(q4==-1)
		{
			Toast.makeText(AddCaseQuestion1.this, "Select Answer for Question 4", Toast.LENGTH_SHORT).show();
			return false;
		}
		
		int q5 = RadioGroup05.getCheckedRadioButtonId();
		System.out.println(q5);
		if(q5==-1)
		{
			Toast.makeText(AddCaseQuestion1.this, "Select Answer for Question 5", Toast.LENGTH_SHORT).show();
			return false;
		}
		return true;
		
		
	}
}

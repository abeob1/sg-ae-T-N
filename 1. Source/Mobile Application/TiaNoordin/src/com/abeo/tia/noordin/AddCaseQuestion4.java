package com.abeo.tia.noordin;

import java.util.ArrayList;
import java.util.HashMap;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import abeo.tia.noordin.R;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

public class AddCaseQuestion4 extends BaseActivity {
	// WebService URL =
	// http://54.251.51.69:3878/SPAMobile.asmx?op=SPA_ListofPropertyEnquiry

	// Find Add Question on web method
	private final String METHOD_ADD_QUESTION = "SPA_AddCase_Questions";

	Button uc, completed;
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	// Find Json Array
	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null;

	// Find Web Service Message
	String messageDisplay = "", StatusResult = "";

	String userName = "", password = "", category = "", qryGroup3 = "", vNDR_RP_FIRM_SELL = "", vNDR_RP_LWYR_SELL = "",
			qryGroup4 = "", vNDR_RP_FIRM_BUY = "", vNDR_RP_LWYR_BUY = "", qryGroup6 = "", qryGroup5 = "",
			qryGroup7 = "", qryGroup8 = "", qryGroup9 = "", qryGroup10 = "", qryGroup11 = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_question4);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find button by Id
		uc = (Button) findViewById(R.id.button_question4Uc);
		completed = (Button) findViewById(R.id.button_question4Completed);

		// Find button on click
		uc.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion4.this, "UC", Toast.LENGTH_SHORT).show();
				// On Sub Sell click save related data in shared preference
				SharedPreferences prefQuest4Uc = getSharedPreferences("AddCaseQuestion4Uc", context.MODE_PRIVATE);
				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuest4Uc.edit();
				edit.putString("QryGroup10", "Y");
				// Commit the change
				edit.clear();
				edit.commit();

				// Find the SharedPreferences value
				SharedPreferences prefQuest4UcReturn = getSharedPreferences("AddCaseQuestion4Uc", Context.MODE_PRIVATE);
				qryGroup10 = prefQuest4UcReturn.getString("QryGroup10", "");
				//Toast.makeText(AddCaseQuestion4.this, "QryGroup10:" + qryGroup10, Toast.LENGTH_SHORT).show();

				// Intent ucIntent = new Intent(AddCaseQuestion4.this,
				// AddCaseStep1of4.class);
				// startActivity(ucIntent);

				// Find addQuestion web method
				addQuestions();

			}
		});

		completed.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion4.this, "Completed", Toast.LENGTH_SHORT).show();
				// On Sub Sell click save related data in shared preference
				SharedPreferences prefQuest4Completed = getSharedPreferences("AddCaseQuestion4Completed",
						context.MODE_PRIVATE);
				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuest4Completed.edit();
				edit.putString("QryGroup11", "Y");
				// Commit the change
				edit.clear();
				edit.commit();

				// Find the SharedPreferences value
				SharedPreferences prefQuest4CompletedReturn = getSharedPreferences("AddCaseQuestion4Completed",
						Context.MODE_PRIVATE);
				qryGroup11 = prefQuest4CompletedReturn.getString("QryGroup11", "");
				// Toast.makeText(AddCaseQuestion4.this, "QryGroup10:" +
				// qryGroup11, Toast.LENGTH_SHORT).show();

				// Intent completedIntent = new Intent(AddCaseQuestion4.this,
				// AddCaseStep1of4.class);
				// startActivity(completedIntent);

				// Find addQuestion web method
				addQuestions();

			}
		});
	}

	public void addQuestions() {
		// Find the SharedPreferences pass Login value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		userName = prefLoginReturn.getString("sUserName", "");
		System.out.println(userName);
		password = prefLoginReturn.getString("sPassword", "");
		System.out.println(password);
		category = prefLoginReturn.getString("sCategory", "");
		System.out.println(category);

		// Find the SharedPreferences question1 sell value
		SharedPreferences prefQuestSellReturn = getSharedPreferences("AddCaseQuestionSell", Context.MODE_PRIVATE);
		System.out.println("SELL DATA");
		qryGroup3 = prefQuestSellReturn.getString("QryGroup3", "");
		System.out.println(qryGroup3);
		vNDR_RP_FIRM_SELL = prefQuestSellReturn.getString("VNDR_RP_FIRM_SELL", "");
		System.out.println(vNDR_RP_FIRM_SELL);
		vNDR_RP_LWYR_SELL = prefQuestSellReturn.getString("VNDR_RP_LWYR_SELL", "");
		System.out.println(vNDR_RP_LWYR_SELL);

		// Find the SharedPreferences question1 buy value
		SharedPreferences prefQuestBuyReturn = getSharedPreferences("AddCaseQuestion1Buy", Context.MODE_PRIVATE);
		System.out.println("BUY DATA");
		qryGroup4 = prefQuestBuyReturn.getString("QryGroup4", "");
		System.out.println(qryGroup4);
		vNDR_RP_FIRM_BUY = prefQuestBuyReturn.getString("VNDR_RP_FIRM_BUY", "");
		System.out.println(vNDR_RP_FIRM_BUY);
		vNDR_RP_LWYR_BUY = prefQuestBuyReturn.getString("VNDR_RP_LWYR_BUY", "");
		System.out.println(vNDR_RP_LWYR_BUY);

		// Find the SharedPreferences question2 Sub Sell value
		SharedPreferences prefQuestSubSellReturn = getSharedPreferences("AddCaseQuestion2SubSell",
				Context.MODE_PRIVATE);
		qryGroup6 = prefQuestSubSellReturn.getString("QryGroup6", "");

		// Find the SharedPreferences question2 developer value
		SharedPreferences prefQuestDeveloperReturn = getSharedPreferences("AddCaseQuestion2Developer",
				Context.MODE_PRIVATE);
		qryGroup5 = prefQuestDeveloperReturn.getString("QryGroup5", "");

		// Find the SharedPreferences question3 commercial value
		SharedPreferences prefQuest3CommercialReturn = getSharedPreferences("AddCaseQuestion3Commercial",
				Context.MODE_PRIVATE);
		qryGroup7 = prefQuest3CommercialReturn.getString("QryGroup7", "");

		// Find the SharedPreferences question3 landed value
		SharedPreferences prefQuest3LandedReturn = getSharedPreferences("AddCaseQuestion3Landed", Context.MODE_PRIVATE);
		qryGroup8 = prefQuest3LandedReturn.getString("QryGroup8", "");

		// Find the SharedPreferences question3 apartment value
		SharedPreferences prefQuest3ApartmentReturn = getSharedPreferences("AddCaseQuestion3Apartment",
				Context.MODE_PRIVATE);
		qryGroup9 = prefQuest3ApartmentReturn.getString("QryGroup9", "");

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

						if (StatusResult.equals("SUCCESS")) {
							Intent qIntent = new Intent(AddCaseQuestion4.this, AddCaseStep1of4.class);
							Toast.makeText(AddCaseQuestion4.this, messageDisplay, Toast.LENGTH_SHORT).show();
							startActivity(qIntent);

						} else {
							Toast.makeText(AddCaseQuestion4.this, messageDisplay, Toast.LENGTH_SHORT).show();

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
}

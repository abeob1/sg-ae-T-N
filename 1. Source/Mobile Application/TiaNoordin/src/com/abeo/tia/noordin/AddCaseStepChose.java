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
import android.widget.TextView;
import android.widget.Toast;

public class AddCaseStepChose extends BaseActivity {
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
		setContentView(R.layout.activity_addcase_chose);
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
		uc = (Button) findViewById(R.id.button_readoffer);
		completed = (Button) findViewById(R.id.button_readtitle);

		// Find button on click
		uc.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseStepChose.this, "UC", Toast.LENGTH_SHORT).show();
				

				 Intent ucIntent = new Intent(AddCaseStepChose.this,
				 AddCaseStep1of4.class);
				startActivity(ucIntent);

				// Find addQuestion web method
				//addQuestions();

			}
		});
		
		// Find button on click
		completed.setOnClickListener(new OnClickListener() {

					@Override
					public void onClick(View v) {
						//Toast.makeText(AddCaseStepChose.this, "UC", Toast.LENGTH_SHORT).show();
						

						 Intent ucIntent = new Intent(AddCaseStepChose.this,
						 AddCaseStep1of4.class);
						startActivity(ucIntent);

						// Find addQuestion web method
						//addQuestions();

					}
				});

		
	}

	
}

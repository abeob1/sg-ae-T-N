package com.abeo.tia.noordin;

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

public class AddCaseQuestion2 extends BaseActivity {
	Button subsell, developer;
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	String  qryGroup6 = "", qryGroup5 = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_question2);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find button by Id
		subsell = (Button) findViewById(R.id.button_question2SubSale);
		developer = (Button) findViewById(R.id.button_question2Developer);

		// Find button on click
		subsell.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {

				Toast.makeText(AddCaseQuestion2.this, "Sub Sell", Toast.LENGTH_SHORT).show();
				// On Sub Sell click save related data in shared preference
				SharedPreferences prefQuestSubSell = getSharedPreferences("AddCaseQuestion2SubSell",
						Context.MODE_PRIVATE);

				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuestSubSell.edit();
				edit.putString("QryGroup6", "Y");

				// Commit the change
				edit.clear();
				edit.commit();

				// Find the SharedPreferences value
				SharedPreferences prefQuestSubSellReturn = getSharedPreferences("AddCaseQuestion2SubSell",
						Context.MODE_PRIVATE);
				qryGroup6 = prefQuestSubSellReturn.getString("QryGroup6", "");

				Toast.makeText(AddCaseQuestion2.this, "QryGroup6:" + qryGroup6, Toast.LENGTH_SHORT).show();
				Intent buyIntent = new Intent(AddCaseQuestion2.this, AddCaseQuestion3.class);
				startActivity(buyIntent);

			}
		});

		developer.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Toast.makeText(AddCaseQuestion2.this, "Developer", Toast.LENGTH_SHORT).show();

				// On Sub developer click save related data in shared preference
				SharedPreferences prefQuestDeveloper = getSharedPreferences("AddCaseQuestion2Developer",
						Context.MODE_PRIVATE);

				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuestDeveloper.edit();
				edit.putString("QryGroup5", "Y");

				// Commit the change
				edit.clear();
				edit.commit();

				// Find the SharedPreferences value
				SharedPreferences prefQuestDeveloperReturn = getSharedPreferences("AddCaseQuestion2Developer",
						Context.MODE_PRIVATE);
				qryGroup5 = prefQuestDeveloperReturn.getString("QryGroup5", "");

				Toast.makeText(AddCaseQuestion2.this, "QryGroup5:" + qryGroup5, Toast.LENGTH_SHORT).show();
				Intent devIntent = new Intent(AddCaseQuestion2.this, AddCaseQuestion3.class);
				startActivity(devIntent);

			}
		});
	}
}

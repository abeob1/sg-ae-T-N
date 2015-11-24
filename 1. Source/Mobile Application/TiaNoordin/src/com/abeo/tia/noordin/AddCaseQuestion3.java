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

public class AddCaseQuestion3 extends BaseActivity {
	Button commercial, landed, apartment;
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	String qryGroup7 = "", qryGroup8 = "", qryGroup9 = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_question3);
		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find button by Id
		commercial = (Button) findViewById(R.id.button_question3Commercial);
		landed = (Button) findViewById(R.id.button_question3Landed);
		apartment = (Button) findViewById(R.id.button_question3Apartment);

		// Find button on click
		commercial.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion3.this, "Commercial", Toast.LENGTH_SHORT).show();

				// On Commercial click save related data in shared preference
				SharedPreferences prefQuest3Commercial = getSharedPreferences("AddCaseQuestion3Commercial",
						Context.MODE_PRIVATE);

				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuest3Commercial.edit();
				edit.putString("QryGroup7", "Y");
				// Commit the change
				edit.clear();
				edit.commit();

				// Find the SharedPreferences value
				SharedPreferences prefQuest3CommercialReturn = getSharedPreferences("AddCaseQuestion3Commercial",
						Context.MODE_PRIVATE);
				qryGroup7 = prefQuest3CommercialReturn.getString("QryGroup7", "");
				//Toast.makeText(AddCaseQuestion3.this, "QryGroup7:" + qryGroup7, Toast.LENGTH_SHORT).show();
				Intent comIntent = new Intent(AddCaseQuestion3.this, AddCaseQuestion4.class);
				startActivity(comIntent);

			}
		});

		landed.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion3.this, "Landed", Toast.LENGTH_SHORT).show();
				// On Landed click save related data in shared preference
				SharedPreferences prefQuest3Landed = getSharedPreferences("AddCaseQuestion3Landed",
						Context.MODE_PRIVATE);
				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuest3Landed.edit();
				edit.putString("QryGroup8", "Y");

				// Commit the change
				edit.clear();
				edit.commit();
				// Find the SharedPreferences value
				SharedPreferences prefQuest3LandedReturn = getSharedPreferences("AddCaseQuestion3Landed",
						Context.MODE_PRIVATE);
				qryGroup8 = prefQuest3LandedReturn.getString("QryGroup8", "");
				//Toast.makeText(AddCaseQuestion3.this, "QryGroup8:" + qryGroup8, Toast.LENGTH_SHORT).show();
				Intent comIntent = new Intent(AddCaseQuestion3.this, AddCaseQuestion4.class);
				startActivity(comIntent);

			}
		});
		apartment.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				//Toast.makeText(AddCaseQuestion3.this, "Apartment", Toast.LENGTH_SHORT).show();
				// On Sub Sell click save related data in shared preference
				SharedPreferences prefQuest3Apartment = getSharedPreferences("AddCaseQuestion3Apartment",
						context.MODE_PRIVATE);
				// We need an editor object to make changes
				SharedPreferences.Editor edit = prefQuest3Apartment.edit();
				edit.putString("QryGroup9", "Y");
				// Commit the change
				edit.clear();
				edit.commit();
				// Find the SharedPreferences value
				SharedPreferences prefQuest3ApartmentReturn = getSharedPreferences("AddCaseQuestion3Apartment",
						Context.MODE_PRIVATE);
				qryGroup9 = prefQuest3ApartmentReturn.getString("QryGroup9", "");
				//Toast.makeText(AddCaseQuestion3.this, "QryGroup9:" + qryGroup9, Toast.LENGTH_SHORT).show();
				Intent apartIntent = new Intent(AddCaseQuestion3.this, AddCaseQuestion4.class);
				startActivity(apartIntent);
			}
		});

	}
}

package com.abeo.tia.noordin;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.res.TypedArray;
import android.os.Bundle;
import abeo.tia.noordin.R;

@SuppressLint("Recycle")
public class FirstActivity extends BaseActivity {
	private String[] navMenuTitles;
	private TypedArray navMenuIcons;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_addcase_question1);
		// load titles from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources().obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);
	}
}

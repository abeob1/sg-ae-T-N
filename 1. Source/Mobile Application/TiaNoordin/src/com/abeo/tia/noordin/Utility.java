package com.abeo.tia.noordin;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Class which has Utility methods
 * 
 */
public class Utility {
	private static Pattern pattern;
	private static Matcher matcher;
	// Email Pattern
	private static final String USER_PATTERN = "^[a-z0-9_-]{3,15}$";

	/**
	 * Validate UserName with regular expression
	 * 
	 * @param UserName
	 * @return true for Valid UserName and false for Invalid Email
	 */
	public static boolean validate(String user_name) {
		pattern = Pattern.compile(USER_PATTERN);
		matcher = pattern.matcher(user_name);
		return matcher.matches();

	}

	/**
	 * Checks for Null String object
	 * 
	 * @param txt
	 * @return true for not null and false for null String object
	 */
	public static boolean isNotNull(String txt) {
		return txt != null && txt.trim().length() > 0 ? true : false;
	}
}
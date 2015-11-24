package com.abeo.tia.noordin;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Locale;

import org.apache.http.Header;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;

import android.app.DatePickerDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.TypedArray;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemSelectedListener;

import abeo.tia.noordin.R;

/**
 * Created by Karthik on 10/7/2015.
 */
public class ProcessCaseLoanPrincipal extends BaseActivity implements
		OnClickListener {

	private String[] navMenuTitles;
	private TypedArray navMenuIcons;
	
	//datepicker
		String myFormat = "MM/dd/yyyy";
		Calendar myCalendar = Calendar.getInstance();

	Button details_btn, purchaser_btn, vendor_btn, property_btn,
			loan_subsidary_btn, process_btn, confirm_btn,walkin;
	
	EditText case_type, file_open_date, case_file_no,LoanDet1,LoanDet2,LoanDet3,LoanDet4,LoanDet5;
	
	EditText req_for_redemption, red_state_date, red_payment_date, loan_case_no, project, branch_name, addr, pa_name, bank_ref, bank_instr_date, letter_offer_date, bank_solicitor, solicitor_loc,
	solicitor_ref, type_facility, facility_amt, repayment, interest_rate, monthly_installment, term_loanamt,
	interest, od_loan, mrta, bank_guarantee, letter_credit, trust_receipt, others;
	String master_bankname;
	
	private final String METHOD_PROCESS_CASE_DETAILS = "SPA_ProcessCase_GetDataFromOCRD";

	JSONArray arrayResponse = null;
	JSONObject jsonResponse = null,jsonResponseconfirm=null;

	// spinner declaration
	Spinner spinner_case_status, spinner_kiv,spinner_LoanTypeOfLoans,spinnerpropertyLSTCHG_BANKNAME;
	TextView ID, TEXT;
	String caseValue_id = "",id_b, name_b, titleValue = "", casetype = "",casetype_value = "",TypeofLoan_value="",Scase_status, Skiv,SLoanTypeOfLoans,bankValue_id,bankValue;

	// checkbox
	CheckBox rep_firm, lawer_rep;
	String f_yes, l_yes;

	// Get Project value fromapi
	private final String GET_SPINNER_VALUES = "SPA_GetValidValues";
	ArrayList<HashMap<String, String>> jsonArraylist = null, jsonlistBank = null;
	String id, name;
	SimpleAdapter sAdapPROJ,sAdapBANK = null;
	String GET_TYPE_SPINNER = "SPA_ProcessCase_GetIDType";
	
	//Confirm btn request URL
	String CONFIRM_BTN_REQUEST = "SPA_ProcessCase_UpdateCaseTabDetails";
	
	String messageDisplay = "", StatusResult = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_process_case_loan_principle);

		// load title from strings.xml
		navMenuTitles = getResources().getStringArray(R.array.nav_drawer_items);
		// load icons from strings.xml
		navMenuIcons = getResources()
				.obtainTypedArray(R.array.nav_drawer_icons);
		set(navMenuTitles, navMenuIcons);

		// Find the SharedPreferences Firstname
					SharedPreferences FirstName = getSharedPreferences("LoginData", Context.MODE_PRIVATE);		
					String FirName = FirstName.getString("FIRSETNAME", "");
					TextView welcome = (TextView)findViewById(R.id.textView_welcome);		
					welcome.setText("Welcome "+FirName);

		// Find the SharedPreferences pass Login value
		SharedPreferences prefLoginReturn = getSharedPreferences("LoginData",
				Context.MODE_PRIVATE);
		System.out.println("LOGIN DATA");
		String userName = prefLoginReturn.getString("sUserName", "");

		String category = prefLoginReturn.getString("sCategory", "");
		System.out.println(category);
		String CardCode = prefLoginReturn.getString("CardCode", "");
		System.out.println(CardCode);

		details_btn = (Button) findViewById(R.id.button_LoanDetails);
		purchaser_btn = (Button) findViewById(R.id.button_LoanPurchaser);
		vendor_btn = (Button) findViewById(R.id.button_LoanVendor);
		property_btn = (Button) findViewById(R.id.button_LoanProperty);
		loan_subsidary_btn = (Button) findViewById(R.id.button_LoanLoanSubsidiary);
		process_btn = (Button) findViewById(R.id.button_LoanProcess);
		confirm_btn = (Button) findViewById(R.id.button_LoanConfirm);
		walkin = (Button) findViewById(R.id.button_LoanWalkin);

		details_btn.setOnClickListener(this);
		purchaser_btn.setOnClickListener(this);
		vendor_btn.setOnClickListener(this);
		property_btn.setOnClickListener(this);
		loan_subsidary_btn.setOnClickListener(this);
		process_btn.setOnClickListener(this);
		confirm_btn.setOnClickListener(this);
		walkin.setOnClickListener(this);
		
		//Edittext
				case_type = (EditText) findViewById(R.id.editText_LoanCaseType);
				case_file_no = (EditText) findViewById(R.id.editText_LoanCaseFileNo);
				file_open_date = (EditText) findViewById(R.id.editText_LoanFileOpenDate);
				req_for_redemption = (EditText) findViewById(R.id.editText_ProcessCase1LA);
				red_state_date = (EditText) findViewById(R.id.editText_LoanRedemptionStatementDate);
				red_payment_date = (EditText) findViewById(R.id.editText_LoanRedemptionPaymentDate);
				loan_case_no = (EditText) findViewById(R.id.editText_LoanCaseNo);
				project = (EditText) findViewById(R.id.editText_LoanProject);
				spinnerpropertyLSTCHG_BANKNAME = (Spinner) findViewById(R.id.banksp);
				branch_name = (EditText) findViewById(R.id.editText_LoanBranchName);
				addr = (EditText) findViewById(R.id.editText_LoanAdddress);
				pa_name = (EditText) findViewById(R.id.editText_LoanPAName);
				bank_ref = (EditText) findViewById(R.id.editText_LoanBankRef);
				bank_instr_date = (EditText) findViewById(R.id.editText_LoanBankInstructionDate);
				letter_offer_date = (EditText) findViewById(R.id.editText_LoanLetterOfOfferDate);
				bank_solicitor = (EditText) findViewById(R.id.editText_LoanBankRefSolicitor);
				solicitor_loc = (EditText) findViewById(R.id.editText_LoanSolicitorLoc);
				solicitor_ref = (EditText) findViewById(R.id.editText_LoanSolicitor);
				type_facility = (EditText) findViewById(R.id.editText_LoanTypeOfFacility);
				facility_amt = (EditText) findViewById(R.id.editText_FacilityAmount);
				repayment = (EditText) findViewById(R.id.editText_LoanRepayment);
				interest_rate = (EditText) findViewById(R.id.editText_LoanInterestRate);
				monthly_installment = (EditText) findViewById(R.id.editText_LoanMonthlyinstallment);
				term_loanamt = (EditText) findViewById(R.id.editText_LoanTermLoanAmount);
				interest = (EditText) findViewById(R.id.editText_LoanBankInsterest);
				od_loan = (EditText) findViewById(R.id.editText_ODLoan);
				mrta = (EditText) findViewById(R.id.editText_LoanMRTA);
				bank_guarantee = (EditText) findViewById(R.id.editText_LoanBankGuarantee);
				letter_credit = (EditText) findViewById(R.id.editText_LetterOfCredit);
				trust_receipt = (EditText) findViewById(R.id.editText_LoanTrustReciept);
				others = (EditText) findViewById(R.id.editText_LoanOthers);
				rep_firm = (CheckBox) findViewById(R.id.LoanFrim);
				
				LoanDet1 = (EditText) findViewById(R.id.editText_LoanDet1);
				LoanDet2 = (EditText) findViewById(R.id.editText_LoanDet2);
				LoanDet3 = (EditText) findViewById(R.id.editText_LoanDet3);
				LoanDet4 = (EditText) findViewById(R.id.editText_LoanDet4);
				LoanDet5 = (EditText) findViewById(R.id.editText_LoanDet5);
				
				
				req_for_redemption.setOnClickListener(this);
				bank_instr_date.setOnClickListener(this);
				red_state_date.setOnClickListener(this);
				red_payment_date.setOnClickListener(this);
				letter_offer_date.setOnClickListener(this);
				
				
				try {
					
					setvaluestoUI();
				} catch (JSONException e) {
					e.printStackTrace();
				}
				
				// spinners initialization
				spinner_case_status = (Spinner) findViewById(R.id.case_status);
				spinner_kiv = (Spinner) findViewById(R.id.spinner_ProcessCase1KIV);
				spinner_LoanTypeOfLoans = (Spinner) findViewById(R.id.spinner_LoanTypeOfLoan);
				// Spinner click listener
						spinner_case_status
								.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

									@Override
									public void onItemSelected(AdapterView<?> parent,
											View view, int position, long id) {
										ID = (TextView) view.findViewById(R.id.Id);
										caseValue_id = ID.getText().toString();
										TEXT = (TextView) view.findViewById(R.id.Name);
										titleValue = TEXT.getText().toString();
									}

									@Override
									public void onNothingSelected(AdapterView<?> parent) {
										// TODO Auto-generated method stub

									}
								});

						
						// Spinner click listener
						spinner_kiv
								.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

									@Override
									public void onItemSelected(AdapterView<?> parent,
											View view, int position, long id) {
										ID = (TextView) view.findViewById(R.id.Id);
										casetype = ID.getText().toString();
										TEXT = (TextView) view.findViewById(R.id.Name);
										casetype_value = TEXT.getText().toString();
									}

									@Override
									public void onNothingSelected(AdapterView<?> parent) {
										// TODO Auto-generated method stub

									}
								});
						

						// Spinner click listener
						spinnerpropertyLSTCHG_BANKNAME.setOnItemSelectedListener(new OnItemSelectedListener() {

									@Override
									public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
										ID = (TextView) view.findViewById(R.id.Id);
										bankValue_id = ID.getText().toString();
										TEXT = (TextView) view.findViewById(R.id.Name);
										bankValue = TEXT.getText().toString();

										// Showing selected spinner item
										//Toast.makeText(parent.getContext(), "Selected: " + developerValue, Toast.LENGTH_LONG).show();

									}

									@Override
									public void onNothingSelected(AdapterView<?> parent) {
										// TODO Auto-generated method stub

									}
								});
						// Spinner click listener
						
						
						// Spinner click listener
						spinner_LoanTypeOfLoans
								.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

									@Override
									public void onItemSelected(AdapterView<?> parent,
											View view, int position, long id) {
										ID = (TextView) view.findViewById(R.id.Id);
										casetype = ID.getText().toString();
										TEXT = (TextView) view.findViewById(R.id.Name);
										TypeofLoan_value = TEXT.getText().toString();
									}

									@Override
									public void onNothingSelected(AdapterView<?> parent) {
										// TODO Auto-generated method stub

									}
								});
						disableHeaderfields();
	}

	
	private void disableHeaderfields() {
		// TODO Auto-generated method stub
	file_open_date.setEnabled(false);
    case_type.setEnabled(false);
    case_file_no.setEnabled(false); 
    spinner_case_status.setEnabled(false);
    spinner_kiv.setEnabled(false);
		
	}
	@Override
	public void onClick(View view) {
		if (view == details_btn) {
			 Intent to_purchaser = new Intent(ProcessCaseLoanPrincipal.this, ProcessCaseDetails.class);
	            startActivity(to_purchaser);
		}
		if (view == purchaser_btn) {
            Intent to_purchaser = new Intent(ProcessCaseLoanPrincipal.this, ProcessCasePurchaser.class);
            startActivity(to_purchaser);
        }
        if (view == vendor_btn) {
            Intent to_vendor = new Intent(ProcessCaseLoanPrincipal.this, ProcessCaseVendor.class);
            startActivity(to_vendor);
        }
        if (view == property_btn) {
            Intent to_loan_pricipal = new Intent(ProcessCaseLoanPrincipal.this, ProcessCaseProperty.class);
            startActivity(to_loan_pricipal);
        }
        
        if (view == loan_subsidary_btn) {
            Intent to_loan_subsidiary = new Intent(ProcessCaseLoanPrincipal.this, ProcesscaseLoanSubsidiary.class);
            startActivity(to_loan_subsidiary);
        }
        if (view == process_btn) {
            Intent to_loan_subsidiary = new Intent(ProcessCaseLoanPrincipal.this, ProcessCaseProcessTab.class);
            startActivity(to_loan_subsidiary);
        }
        if(view == walkin)
        {
        	Intent i = new Intent(ProcessCaseLoanPrincipal.this, WalkInActivity.class);
			startActivity(i);
        }
  
		if(view == req_for_redemption)
        {
			 new DatePickerDialog(ProcessCaseLoanPrincipal.this, req_for_redemption1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(view == bank_instr_date)
        {
			 new DatePickerDialog(ProcessCaseLoanPrincipal.this, bank_instr_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(view == red_state_date)
        {
			 new DatePickerDialog(ProcessCaseLoanPrincipal.this, red_state_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(view == red_payment_date)
        {
			 new DatePickerDialog(ProcessCaseLoanPrincipal.this, red_payment_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if(view == letter_offer_date)
        {
			 new DatePickerDialog(ProcessCaseLoanPrincipal.this, letter_offer_date1, myCalendar
	                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
	                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        }	
		if (view == confirm_btn) {
			try {
				confirm_allvalues_btn();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}
	
	
	DatePickerDialog.OnDateSetListener req_for_redemption1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        req_for_redemption.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener bank_instr_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        bank_instr_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener red_state_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        red_state_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener red_payment_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        red_payment_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	
	DatePickerDialog.OnDateSetListener letter_offer_date1 = new DatePickerDialog.OnDateSetListener() {

	    @Override
	    public void onDateSet(DatePicker view, int year, int monthOfYear,
	            int dayOfMonth) {
	        // TODO Auto-generated method stub
	        myCalendar.set(Calendar.YEAR, year);
	        myCalendar.set(Calendar.MONTH, monthOfYear);
	        myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
	        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.getDefault());	        
	        letter_offer_date.setText(sdf.format(myCalendar.getTime()));
	    }

	};
	private void confirm_allvalues_btn() throws JSONException {
		// TODO Auto-generated method stub
		
		
		String json_element = "[{\"Case\":" + "\"" + jsonResponseconfirm.get("Case").toString() + "\",\"CaseType\":" + "\"" + jsonResponseconfirm.get("CaseType").toString() + "\",\"CaseStatus\":\"OPEN\",\"FileOpenDate\":\"10/10/2015 12:00:00 AM\",\"CaseFileNo\":\"JJ/1500000001/\",\"KIV\":\"\",\"TabId\":\"5\",\"Details\":{\"LA\":\"\",\"MANAGER\":\"\",\"InCharge\":\"\",\"CustomerService\":\"\",\"CaseType\":\"SPA-BUY-DEV-APT-C\",\"FileLocation\":\"\",\"FileClosedDate\":\"\",\"VendAcqDt\":\"\",\"CompanyBuisnessSearch\":\"\",\"BankWindingSearch\":\"\"},\"Purchaser\":{\"PurRepresentedByFirm\":\"N\",\"PurlawyerRepresented\":\"N\",\"PurSPADate\":\"1/1/1900 12:00:00 AM\",\"PurEntryOfPrivateCaveat\":\"Chase\",\"PurWithOfPrivateCaveat\":\"Rane\",\"PurFirstName\":\"Name1\",\"PurFirstID\":\"Id1\",\"PurFirstTaxNo\":\"Tax1\",\"PurFirstContactNo\":\"9784561233\",\"PurFirstType\":\"CORPORATE\",\"PurSecName\":\"Name2\",\"PurSecID\":\"Id2\",\"PurSecTaxNo\":\"Tax2\",\"PurSecContactNo\":\"9784561234\",\"PurSecType\":\"INDIVIDUAL\",\"PurThirdName\":\"Name3\",\"PurThirdID\":\"Id3\",\"PurThirdTaxNo\":\"Tax3\",\"PurThirdContactNo\":\"9784561234\",\"PurThirdType\":\"INDIVIDUAL\",\"PurFourthName\":\"Name4\",\"PurFourthID\":\"Id4\",\"PurFourthTaxNo\":\"Tax4\",\"PurFourthContactNo\":\"9784561235\",\"PurFourthType\":\"INDIVIDUAL\"},\"Vendor\":{\"VndrRepresentedByFirm\":\"N\",\"VndrlawyerRepresented\":\"Y\",\"VndrReqDevConsent\":\"Name\",\"VndrReceiveDevConsent\":\"Sam\",\"VndrFirstName\":\"Name1\",\"VndrFirstID\":\"Id1\",\"VndrFirstTaxNo\":\"Tax1\",\"VndrFirstContactNo\":\"9784561233\",\"VndrFirstType\":\"CORPORATE\",\"VndrSecName\":\"Name2\",\"VndrSecID\":\"Id2\",\"VndrSecTaxNo\":\"Tax2\",\"VndrSecContactNo\":\"9784561234\",\"VndrSecType\":\"INDIVIDUAL\",\"VndrThirdName\":\"Name3\",\"VndrThirdID\":\"Id3\",\"VndrThirdTaxNo\":\"Tax3\",\"VndrThirdContactNo\":\"9784561234\",\"VndrThirdType\":\"INDIVIDUAL\",\"VndrFourthName\":\"Name4\",\"VndrFourthID\":\"Id4\",\"VndrFourthTaxNo\":\"Tax4\",\"VndrFourthContactNo\":\"9784561235\",\"VndrFourthType\":\"INDIVIDUAL\"},\"Property\":{\"TitleType\":\"KEKAL\",\"CertifiedPlanNo\":\"\",\"LotNo\":\"\",\"PreviouslyKnownAs\":\"\",\"State\":\"\",\"Area\":\"\",\"BPM\":\"\",\"GovSurvyPlan\":\"\",\"LotArea\":\"12345\",\"Developer\":\"\",\"Project\":\"\",\"DevLicenseNo\":\"\",\"DevSolicitor\":\"\",\"DevSoliLoc\":\"\",\"TitleSearchDate\":\"\",\"DSCTransfer\":\"\",\"DRCTransfer\":\"\",\"FourteenADate\":\"\",\"DRTLRegistry\":\"\",\"PropertyCharged\":\"\",\"BankName\":\"\",\"Branch\":\"\",\"PAName\":\"\",\"PresentationNo\":\"\",\"ExistChargeRef\":\"\",\"ReceiptType\":\"\",\"ReceiptNo\":\"\",\"ReceiptDate\":\"\",\"PurchasePrice\":\"1452\",\"AdjValue\":\"\",\"VndrPrevSPAValue\":\"\",\"Deposit\":\"\",\"BalPurPrice\":\"\",\"LoanAmount\":\"\",\"LoanCaseNo\":\"1234\",\"DiffSum\":\"\",\"RedAmt\":\"\",\"RedDate\":\"\",\"DefRdmptSum\":\"\"},\"LoanPrinciple\":{\"" + "ReqRedStatement" + "\":" + "\"" + req_for_redemption.getText().toString() + "\",\"" + "RedStmtDate" + "\":" + "\"" + red_state_date.getText().toString() + "\",\"" + "RedPayDate" + "\":" + "\"" + red_payment_date.getText().toString() + "\",\"RepByFirm\":"+"\""+rep_firm.isChecked()+"\",\"" + "LoanCaseNo" + "\":" + "\"" + loan_case_no.getText().toString() + "\",\"" + "Project" + "\":" + "\"" + project.getText().toString() + "\",\"" + "MasterBankName" + "\":" + "\"" + bankValue + "\",\"" + "BranchName" + "\":" + "\"" + branch_name.getText().toString() + "\",\"" + "Address" + "\":" + "\"" + addr.getText().toString() + "\",\"" + "PAName" + "\":" + "\"" + pa_name.getText().toString() + "\",\"" + "BankRef" + "\":" + "\"" + bank_ref.getText().toString() + "\",\"" + "BankInsDate" + "\":" + "\"" + bank_instr_date.getText().toString() + "\",\"" + "LOFDate" + "\":" + "\"" + letter_offer_date.getText().toString() + "\",\"" + "BankSolicitor" + "\":" + "\"" + bank_solicitor.getText().toString() + "\",\"" + "SoliLoc" + "\":" + "\"" + solicitor_loc.getText().toString() + "\",\"" + "SoliRef" + "\":" + "\"" + solicitor_ref.getText().toString() + "\",\"TypeofLoan\":" + "\"" + TypeofLoan_value + "\",\"" + "TypeofFacility" + "\":" + "\"" + type_facility.getText().toString() + "\",\"" + "FacilityAmt" + "\":" + "\"" + facility_amt.getText().toString() + "\",\"" + "Repaymt" + "\":" + "\"" + repayment.getText().toString() + "\",\"" + "IntrstRate" + "\":" + "\"" + interest_rate.getText().toString() + "\",\"" + "MonthlyInstmt" + "\":" + "\"" + monthly_installment.getText().toString() + "\",\"" + "TermLoanAmt" + "\":" + "\"" + term_loanamt.getText().toString() + "\",\"" + "Interest" + "\":" + "\"" + interest.getText().toString() + "\",\"" + "ODLoan" + "\":" + "\"" + od_loan.getText().toString() + "\",\"" + "MRTA" + "\":" + "\"" + mrta.getText().toString() + "\",\"" + "BankGuarantee" + "\":" + "\"" + bank_guarantee.getText().toString() + "\",\"" + "LetterofCredit" + "\":" + "\"" + letter_credit.getText().toString() + "\",\"" + "TrustReceipt" + "\":" + "\"" + trust_receipt.getText().toString() + "\",\"" + "Others" + "\":" + "\"" + others.getText().toString() + "\",\"LoanDet1\":" + "\"" + LoanDet1.getText().toString() + "\",\"LoanDet2\":" + "\"" + LoanDet2.getText().toString() + "\",\"LoanDet3\":" + "\"" + LoanDet3.getText().toString() + "\",\"LoanDet4\":" + "\"" + LoanDet4.getText().toString() + "\",\"LoanDet5\":" + "\"" + LoanDet5.getText().toString() + "\"},\"LoanSubsidary\":{\"LoanDocForBankExe\":\"Exe 1\",\"FaciAgreeDate\":\"\",\"LoanDocRetFromBank\":\"\",\"DischargeofCharge\":\"\",\"FirstTypeofFacility\":\"\",\"FirstFacilityAmt\":\"\",\"FirstRepaymt\":\"\",\"FirstIntrstRate\":\"\",\"FirstMonthlyInstmt\":\"1500\",\"FirstTermLoanAmt\":\"\",\"FirstInterest\":\"\",\"FirstODLoan\":\"\",\"FirstMRTA\":\"\",\"FirstBankGuarantee\":\"\",\"FirstLetterofCredit\":\"\",\"FirstTrustReceipt\":\"\",\"FirstOthers\":\"Sample\",\"SecTypeofFacility\":\"\",\"SecFacilityAmt\":\"\",\"SecRepaymt\":\"\",\"SecIntrstRate\":\"12%\",\"SecMonthlyInstmt\":\"\",\"SecTermLoanAmt\":\"\",\"SecInterest\":\"\",\"SecODLoan\":\"\",\"SecMRTA\":\"\",\"SecBankGuarantee\":\"\",\"SecLetterofCredit\":\"\",\"SecTrustReceipt\":\"45A\",\"SecOthers\":\"\",\"ThirdTypeofFacility\":\"Sample_Data\",\"ThirdFacilityAmt\":\"\",\"ThirdRepaymt\":\"\",\"ThirdIntrstRate\":\"\",\"ThirdMonthlyInstmt\":\"\",\"ThirdTermLoanAmt\":\"587.15\",\"ThirdInterest\":\"\",\"ThirdODLoan\":\"\",\"ThirdMRTA\":\"\",\"ThirdBankGuarantee\":\"\",\"ThirdLetterofCredit\":\"\",\"ThirdTrustReceipt\":\"\",\"ThirdOthers\":\"\",\"FourthTypeofFacility\":\"Sample4\",\"FourthFacilityAmt\":\"\",\"FourthRepaymt\":\"15\",\"FourthIntrstRate\":\"\",\"FourthMonthlyInstmt\":\"\",\"FourthTermLoanAmt\":\"\",\"FourthInterest\":\"21\",\"FourthODLoan\":\"\",\"FourthMRTA\":\"\",\"FourthBankGuarantee\":\"\",\"FourthLetterofCredit\":\"\",\"FourthTrustReceipt\":\"\",\"FourthOthers\":\"\",\"FifthTypeofFacility\":\"Sample 5\",\"FifthFacilityAmt\":\"\",\"FifthRepaymt\":\"\",\"FifthIntrstRate\":\"\",\"FifthMonthlyInstmt\":\"\",\"FifthTermLoanAmt\":\"\",\"FifthInterest\":\"10\",\"FifthODLoan\":\"\",\"FifthMRTA\":\"\",\"FifthBankGuarantee\":\"\",\"FifthLetterofCredit\":\"\",\"FifthTrustReceipt\":\"Test\",\"FifthOthers\":\"\"}}]";
		Log.e("pur_string", json_element);
		
		JSONObject valuesObject = null;
		JSONArray list = null;
	try {		
		list = new JSONArray(json_element);
		 valuesObject = list.getJSONObject(0);
		
			/*valuesObject.put("PurSPADate",spa_date.getText().toString());
			valuesObject.put("PurEntryOfPrivateCaveat", entry_private_caveat.getText().toString());
			valuesObject.put("PurWithOfPrivateCaveat", withdrawal_private_caveat.getText().toString());
			valuesObject.put("PurFirstName", name1.getText().toString());
			valuesObject.put("PurFirstID", brn_no1.getText().toString());
			valuesObject.put("PurFirstTaxNo", tax_no1.getText().toString());
			valuesObject.put("PurFirstContactNo", contact_no1.getText().toString());*/
			
			//list.put(valuesObject);
			
			
	} catch (JSONException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
	}
		
	RequestParams params = new RequestParams();
	params.put("sJsonInput", list.toString());
	System.out.println("params");

	RestService.post(CONFIRM_BTN_REQUEST, params, new BaseJsonHttpResponseHandler<String>() {

		@Override
		public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
			// TODO Auto-generated method stub
			System.out.println(arg3);
			System.out.println("onFailure process case purchaser");
		}

		@Override
		public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
			// TODO Auto-generated method stub
			System.out.println("Add purchaser Confirmed");
			System.out.println(arg2);

			// Find status Response
			try {
				StatusResult = jsonResponse.getString("Result").toString();
				messageDisplay = jsonResponse.getString("DisplayMessage").toString();
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			if (StatusResult.equals("SUCCESS")) {
				Intent iAddBack = new Intent(ProcessCaseLoanPrincipal.this, ProcesscaseLoanSubsidiary.class);
				startActivity(iAddBack);
				Toast.makeText(ProcessCaseLoanPrincipal.this, messageDisplay, Toast.LENGTH_SHORT).show();
			} else {
				Toast.makeText(ProcessCaseLoanPrincipal.this, messageDisplay, Toast.LENGTH_SHORT).show();

			}
		}

		@Override
		protected String parseResponse(String arg0, boolean arg1) throws Throwable {

			// Get Json response
			arrayResponse = new JSONArray(arg0);
			jsonResponse = arrayResponse.getJSONObject(0);

			System.out.println("Purchaser Add Response");
			System.out.println(arg0);
			return null;
		}
	});			
	}
	

	public void dropdownBankDeveloperSolicitor() {

		RequestParams params = null;
		params = new RequestParams();

		RestService.post("SPA_Property_GetDropdownValues", params, new BaseJsonHttpResponseHandler<String>() {

			@Override
			public void onFailure(int arg0, Header[] arg1, Throwable arg2, String arg3, String arg4) {
				// TODO Auto-generated method stub
				System.out.println(arg3);
			}

			@Override
			public void onSuccess(int arg0, Header[] arg1, String arg2, String arg3) {
				// TODO Auto-generated method stub
				System.out.println("Property Activity GetDropdown Success Details ");
				try {
					// Create new list
					jsonlistBank = new ArrayList<HashMap<String, String>>();
					

					jsonResponse = new JSONObject(arg2);

					JSONArray jsonBank = jsonResponse.getJSONArray("Bank");
					for (int j = 0; j < jsonBank.length(); j++) {
						JSONObject bank = jsonBank.getJSONObject(j);

						id_b = bank.getString("BankCode").toString();
						name_b = bank.getString("BankName").toString();

						// SEND JSON DATA INTO SPINNER TITLE LIST
						HashMap<String, String> bankList = new HashMap<String, String>();

						// Send JSON Data to list activity
						System.out.println("SEND JSON BANK LIST");
						bankList.put("Id_T", id_b);
						System.out.println(name);
						bankList.put("Name_T", name_b);
						System.out.println(name);
						System.out.println(" END SEND JSON BANK LIST");

						jsonlistBank.add(bankList);
						System.out.println("JSON BANK LIST");
						//System.out.println(jsonlistProject);

					}

					// Spinner set Array Data in Drop down

					sAdapBANK = new SimpleAdapter(ProcessCaseLoanPrincipal.this, jsonlistBank, R.layout.spinner_item,
							new String[] { "Id_T", "Name_T" }, new int[] { R.id.Id, R.id.Name });

					spinnerpropertyLSTCHG_BANKNAME.setAdapter(sAdapBANK);

					for (int j = 0; j < jsonlistBank.size(); j++) {
						if (jsonlistBank.get(j).get("Name_T").equals(master_bankname)) {
							spinnerpropertyLSTCHG_BANKNAME.setSelection(j);
							break;
						}
					}


				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

				System.out.println(arg2);

			}

			@Override
			protected String parseResponse(String arg0, boolean arg1) throws Throwable {
				// Get Json response

				System.out.println("Property GetDropdown parse Response");
				System.out.println(arg0);
				return null;
			}

		});

	}

	
	
	public void dropdownLTY() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "LOAN_PRNCP_TYPE");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_SPINNER_VALUES, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Title Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylist = new ArrayList<HashMap<String, String>>();

							for (int i = 0; i < arrayResponse.length(); i++) {

								jsonResponse = arrayResponse.getJSONObject(i);

								id = jsonResponse.getString("Id").toString();
								name = jsonResponse.getString("Name")
										.toString();

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								proList.put("Id_T", id);
								System.out.println(name);
								proList.put("Name_T", name);
								System.out.println(name);
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCaseLoanPrincipal.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_LoanTypeOfLoans.setAdapter(sAdapPROJ);

							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(SLoanTypeOfLoans)) {
								  spinner_LoanTypeOfLoans.setSelection(j); break; } }

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	
	public void dropdownKIV() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "KIVSTATUS");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_SPINNER_VALUES, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Title Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylist = new ArrayList<HashMap<String, String>>();

							for (int i = 0; i < arrayResponse.length(); i++) {

								jsonResponse = arrayResponse.getJSONObject(i);

								id = jsonResponse.getString("Id").toString();
								name = jsonResponse.getString("Name")
										.toString();

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								proList.put("Id_T", id);
								System.out.println(name);
								proList.put("Name_T", name);
								System.out.println(name);
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCaseLoanPrincipal.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_kiv.setAdapter(sAdapPROJ);

							
							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(Skiv)) {
								  spinner_kiv.setSelection(j); break; } }

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	public void dropdownstatus() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		JSONObject jsonObject = new JSONObject();
		jsonObject.put("TableName", "OCRD");
		jsonObject.put("FieldName", "CASESTATUS");
		params.put("sJsonInput", jsonObject.toString());

		RestService.post(GET_SPINNER_VALUES, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					protected String parseResponse(String s, boolean b)
							throws Throwable {
						return null;
					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out.println("Title Dropdown Success Details ");
						System.out.println(arg2);

						try {

							arrayResponse = new JSONArray(arg2);
							// Create new list
							jsonArraylist = new ArrayList<HashMap<String, String>>();

							for (int i = 0; i < arrayResponse.length(); i++) {

								jsonResponse = arrayResponse.getJSONObject(i);

								id = jsonResponse.getString("Id").toString();
								name = jsonResponse.getString("Name")
										.toString();

								// SEND JSON DATA INTO SPINNER TITLE LIST
								HashMap<String, String> proList = new HashMap<String, String>();

								// Send JSON Data to list activity
								System.out.println("SEND JSON  LIST");
								proList.put("Id_T", id);
								System.out.println(name);
								proList.put("Name_T", name);
								System.out.println(name);
								System.out
										.println(" END SEND JSON PROPERTY LIST");

								jsonArraylist.add(proList);
								System.out.println("JSON PROPERTY LIST");
								System.out.println(jsonArraylist);
							}
							// Spinner set Array Data in Drop down

							sAdapPROJ = new SimpleAdapter(
									ProcessCaseLoanPrincipal.this, jsonArraylist,
									R.layout.spinner_item, new String[] {
											"Id_T", "Name_T" }, new int[] {
											R.id.Id, R.id.Name });

							spinner_case_status.setAdapter(sAdapPROJ);

							for (int j = 0; j < jsonArraylist.size(); j++)
							  { if (jsonArraylist.get(j).get("Id_T").equals(Scase_status)) {
								  spinner_case_status.setSelection(j); break; } }

						} catch (JSONException e) {

							e.printStackTrace();
						}
					}
				});
	}
	private void setvaluestoUI() throws JSONException {
		RequestParams params = null;
		params = new RequestParams();

		// Find the SharedPreferences value
				SharedPreferences prefLoginReturn = getSharedPreferences("LoginData", Context.MODE_PRIVATE);
				System.out.println("LOGIN DATA");
				user_name = prefLoginReturn.getString("sUserName", "");
				System.out.println(user_name);
				Pswd = prefLoginReturn.getString("sPassword", "");
				System.out.println(Pswd);
				catg = prefLoginReturn.getString("sCategory", "");
				System.out.println(catg);
				sUserRole = prefLoginReturn.getString("sUserRole", "");
				System.out.println(sUserRole);
				String sCaseNo = prefLoginReturn.getString("CaseNo", "");
				System.out.println(sCaseNo);

		        final JSONObject jsonObject = new JSONObject();
		        jsonObject.put("CaseNo", sCaseNo);
		        jsonObject.put("UserName", user_name);
		        jsonObject.put("UserRole", sUserRole);
		        params.put("sJsonInput", jsonObject.toString());

		RestService.post(METHOD_PROCESS_CASE_DETAILS, params,
				new BaseJsonHttpResponseHandler<String>() {

					@Override
					public void onFailure(int arg0, Header[] arg1,
							Throwable arg2, String arg3, String arg4) {
						// TODO Auto-generated method stub
						System.out.println(arg3);
						System.out.println("Failed");

					}

					@Override
					public void onSuccess(int arg0, Header[] arg1, String arg2,
							String arg3) {
						// TODO Auto-generated method stub
						System.out
								.println("property Dropdown Success Details ");
						System.out.println(arg2);
						try {
							JSONArray arrayRes = new JSONArray(arg2);
							jsonResponseconfirm =arrayRes.getJSONObject(0);
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}

						setallvalues(arg2);

					}

					@Override
					protected String parseResponse(String arg0, boolean arg1)
							throws Throwable {

						// Get Json response
						JSONArray arrayResponse = new JSONArray(arg0);
						jsonResponse = arrayResponse.getJSONObject(0);

						System.out
								.println("Property Dropdown Details parse Response");
						System.out.println(arg0);
						return null;
					}
				});

	}

	private void setallvalues(String arg2) {

		JSONArray jObj = null;
		try {
			jObj = new JSONArray(arg2.toString());

			for (int i = 0; i < jObj.length(); i++) {
				JSONObject jsonobject = jObj.getJSONObject(i);
				file_open_date.setText(jsonobject.getString("FileOpenDate"));
				case_file_no.setText(jsonobject.getString("CaseFileNo"));
				case_type.setText(jsonobject.getString("CaseType"));
				Scase_status = jsonobject.getString("CaseStatus");

				JSONObject obj1 = jsonobject.getJSONObject("LoanPrinciple");

				req_for_redemption.getText().toString();
				
				req_for_redemption.setText(obj1.getString("ReqRedStatement"));
				red_state_date.setText(obj1.getString("RedStmtDate"));
				red_payment_date.setText(obj1.getString("RedPayDate"));
				loan_case_no.setText(obj1
						.getString("LoanCaseNo"));
				project.setText(obj1
						.getString("Project"));
				master_bankname = obj1.getString("MasterBankName");
				branch_name.setText(obj1.getString("BranchName"));
				addr.setText(obj1.getString("Address"));
				pa_name.setText(obj1.getString("PAName"));
				bank_ref.setText(obj1.getString("BankRef"));
				bank_instr_date.setText(obj1.getString("BankInsDate"));
				letter_offer_date.setText(obj1.getString("LOFDate"));
				bank_solicitor.setText(obj1.getString("BankSolicitor"));
				solicitor_loc.setText(obj1.getString("SoliLoc"));
				solicitor_ref.setText(obj1.getString("SoliRef"));
				type_facility.setText(obj1.getString("TypeofFacility"));
				facility_amt.setText(obj1.getString("FacilityAmt"));
				repayment.setText(obj1.getString("Repaymt"));
				interest_rate.setText(obj1.getString("IntrstRate"));
				monthly_installment.setText(obj1.optString("MonthlyInstmt"));
				term_loanamt.setText(obj1.getString("TermLoanAmt"));
				interest.setText(obj1.optString("Interest"));
				od_loan.setText(obj1.getString("ODLoan"));
				mrta.setText(obj1.getString("MRTA"));
				bank_guarantee.setText(obj1.optString("BankGuarantee"));
				letter_credit.setText(obj1.getString("LetterofCredit"));
				trust_receipt.setText(obj1.optString("TrustReceipt"));
				others.setText(obj1.getString("Others"));
				
				LoanDet1.setText(obj1.getString("LoanDet1"));
				LoanDet2.setText(obj1.getString("LoanDet2"));
				LoanDet3.setText(obj1.getString("LoanDet3"));
				LoanDet4.setText(obj1.getString("LoanDet4"));
				LoanDet5.setText(obj1.getString("LoanDet5"));
				
				Skiv = jsonobject.getString("KIV");
				SLoanTypeOfLoans = obj1.getString("TypeofLoan");
				

				if (obj1.getString("RepByFirm").toString().equals("Y")) {
					rep_firm.setChecked(true);
				} else {
					rep_firm.setChecked(false);
				}
			}
			System.out.println(jObj);
			
			dropdownstatus();
			dropdownKIV();
			dropdownLTY();
			dropdownBankDeveloperSolicitor();
		} catch (JSONException e) {
			e.printStackTrace();
		}
		
		
	}

	public boolean dispatchTouchEvent(MotionEvent ev) {
		InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), 0);
		return super.dispatchTouchEvent(ev);
	}
}

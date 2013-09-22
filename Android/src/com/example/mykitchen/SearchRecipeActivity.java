package com.example.mykitchen;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import com.example.mykitchen.LoginActivity.UserLoginTask;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.support.v4.app.NavUtils;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.SeekBar;
import android.widget.TextView;

public class SearchRecipeActivity extends Activity {

	private int time;
	private int budget;
	private int health;

	private Intent intent;

	private PostTask mPostTask = null;

	private View mPostStatusView;
	private TextView mPostStatusMessageView;
	
	private Application application = this.getApplication();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_search_recipe);
		// Show the Up button in the action bar.
		setupActionBar();

		mPostStatusView = findViewById(R.id.post_status);
		mPostStatusMessageView = (TextView) findViewById(R.id.post_status_message);

		SeekBar timeBar = (SeekBar)findViewById(R.id.seekBarT);
		SeekBar budgetBar = (SeekBar)findViewById(R.id.seekBarB);
		SeekBar healthBar = (SeekBar)findViewById(R.id.seekBarH);

		final TextView timeBarValue = (TextView)findViewById(R.id.timerCount);



		timeBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
			@Override
			public void onProgressChanged(SeekBar seekbar, int progress, boolean fromUser) {
				timeBarValue.setText(String.valueOf(progress) + " min");
				time = progress;
			}

			@Override
			public void onStartTrackingTouch(SeekBar seekBar) {
			}

			@Override
			public void onStopTrackingTouch(SeekBar seekBar) {
			}
		});

		budgetBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
			@Override
			public void onProgressChanged(SeekBar seekbar, int progress, boolean fromUser) {
				budget = progress;
			}

			@Override
			public void onStartTrackingTouch(SeekBar seekBar) {
			}

			@Override
			public void onStopTrackingTouch(SeekBar seekBar) {
			}
		});

		healthBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
			@Override
			public void onProgressChanged(SeekBar seekbar, int progress, boolean fromUser) {
				health = progress;
			}

			@Override
			public void onStartTrackingTouch(SeekBar seekBar) {
			}

			@Override
			public void onStopTrackingTouch(SeekBar seekBar) {
			}
		});

		findViewById(R.id.buttonSearchSearch).setOnClickListener(
				new View.OnClickListener() {
					@Override
					public void onClick(View view) {
						attemptPost();
					}
				});
	}

	/**
	 * Shows the progress UI and hides the login form.
	 */
	@TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
	private void showProgress(final boolean show) {
		// On Honeycomb MR2 we have the ViewPropertyAnimator APIs, which allow
		// for very easy animations. If available, use these APIs to fade-in
		// the progress spinner.
		if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
			int shortAnimTime = getResources().getInteger(
					android.R.integer.config_shortAnimTime);

			mPostStatusView.setVisibility(View.VISIBLE);
			mPostStatusView.animate().setDuration(shortAnimTime)
			.alpha(show ? 1 : 0)
			.setListener(new AnimatorListenerAdapter() {
				@Override
				public void onAnimationEnd(Animator animation) {
					mPostStatusView.setVisibility(show ? View.VISIBLE
							: View.GONE);
				}
			});
		} else {
			// The ViewPropertyAnimator APIs are not available, so simply show
			// and hide the relevant UI components.
			mPostStatusView.setVisibility(show ? View.VISIBLE : View.GONE);
		}
	}

	public void attemptPost() {
		intent = new Intent(this, SearchListActivity.class);

		showProgress(true);
		mPostTask= new PostTask();
		mPostTask.execute((Void) null);
	}

	public class PostTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected Boolean doInBackground(Void... params) {
			HttpClient httpclient = new DefaultHttpClient();
			HttpPost httppost = new HttpPost(getString(R.string.webServer) + "/Users/SearchRecipes");
			
			try {
				List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
				nameValuePairs.add(new BasicNameValuePair("userIdStr", String.valueOf(((MyApplication) application).getUserId())));
				nameValuePairs.add(new BasicNameValuePair("budgetStr", String.valueOf(budget)));
				nameValuePairs.add(new BasicNameValuePair("alottedTimeStr", String.valueOf(time)));
				nameValuePairs.add(new BasicNameValuePair("healthinessStr", String.valueOf(health)));
				
				httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));

				HttpResponse response = httpclient.execute(httppost);
				BufferedReader reader = new BufferedReader(new InputStreamReader(response.getEntity().getContent(), "UTF-8"));
				ArrayList<Integer> array = new ArrayList<Integer>();
				while (reader.readLine() != null) {
					String json = reader.readLine();
					array.add(Integer.parseInt(json));
				}
					
				((MyApplication) application).setRecipes(array);
			}
			catch (ClientProtocolException e) {
				return false;
			} catch (IOException e) {
				return false;
			}
			
			startActivity(intent);
			return true;
		}
	}

	/**
	 * Set up the {@link android.app.ActionBar}, if the API is available.
	 */
	@TargetApi(Build.VERSION_CODES.HONEYCOMB)
	private void setupActionBar() {
		if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
			getActionBar().setDisplayHomeAsUpEnabled(true);
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.search_recipe, menu);
		return true;
	}

	public void onClick(View view){
		Intent intent = new Intent(this, SearchListActivity.class);
		startActivity(intent);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case android.R.id.home:
			// This ID represents the Home or Up button. In the case of this
			// activity, the Up button is shown. Use NavUtils to allow users
			// to navigate up one level in the application structure. For
			// more details, see the Navigation pattern on Android Design:
			//
			// http://developer.android.com/design/patterns/navigation.html#up-vs-back
			//
			NavUtils.navigateUpFromSameTask(this);
			return true;
		}
		return super.onOptionsItemSelected(item);
	}	

}

package com.example.moneymanager;

import android.annotation.SuppressLint;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class SQLiteManager extends SQLiteOpenHelper {
    private static SQLiteManager sqLiteManager;

    private static final String DATABASE_NAME = "MoneyManagerDB";
    private static final String COUNTER = "Counter";

    private static final int DATABASE_VERSION = 1;


    private static final String USERS_TABLE_NAME = "Users";
    private static final String USERS_ID_FIELD = "users_ID";
    private static final String USERS_NAME_FIELD = "users_name";
    private static final String USERS_PHONE_NUMBER_FIELD = "users_phone_number";
    private static final String USERS_EMAIL_FIELD = "users_email";
    private static final String PASSWORD_HASH_FIELD = "password_hash";
    private static final String PASSWORD_SALT_FIELD = "users_salt";
    private static final String USERS_PHOTO_FIELD = "users_photo";


    private static final String ACCOUNTS_TABLE_NAME = "Accounts";
    private static final String ACCOUNTS_ID_FIELD = "accounts_ID";
    private static final String ACCOUNTS_TITLE_FIELD = "accounts_title";
    private static final String FK_USERS_ID_FIELD = "fk_users_ID";


    private static final String GOALS_TABLE_NAME = "Goals";
    private static final String GOALS_ID_FIELD = "goals_ID";
    private static final String GOALS_TITLE_FIELD = "goals_title";
    private static final String GOALS_DESCRIPTION_FIELD = "goals_description";
    private static final String GOALS_AMOUNT_TO_COLLECT_FIELD = "goals_AmountToCollect";
    private static final String FK_ACCOUNTS_ID_FIELD = "fk_accounts_ID";


    private static final String TRANSACTIONS_TABLE_NAME = "Transactions";
    private static final String TRANSACTIONS_ID_FIELD = "transaction_ID";
    private static final String TRANSACTIONS_TYPE_FIELD = "transaction_type";
    private static final String FK_ACCOUNTS_ID_FROM_FIELD = "fk_accounts_ID_from";
    private static final String FK_ACCOUNTS_ID_TO_FIELD = "fk_accounts_ID_to";
    private static final String TRANSACTIONS_DESCRIPTION_FIELD = "transactions_description";
    private static final String TRANSACTIONS_SUM_FIELD = "transactions_sum";
    private static final String TRANSACTIONS_DATE_FIELD = "transactions_date";


    @SuppressLint("SimpleDateFormat")
    private static final DateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy HH:mm:ss");

    public SQLiteManager(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    public static SQLiteManager instanceOfDatabase(Context context) {
        if(sqLiteManager==null)
            sqLiteManager = new SQLiteManager(context);

        return sqLiteManager;
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        StringBuilder sql_users_table;
        sql_users_table = new StringBuilder()
                .append("CREATE TABLE ")
                .append(USERS_TABLE_NAME)
                .append("(")
                .append(USERS_ID_FIELD)
                // .append(" SERIAL,")
                .append(" INTEGER PRIMARY KEY AUTOINCREMENT, ")
                .append(USERS_NAME_FIELD)
                .append(" VARCHAR, ")
                .append(USERS_PHONE_NUMBER_FIELD)
                .append(" VARCHAR, ")
                .append(USERS_EMAIL_FIELD)
                .append(" VARCHAR, ")
                .append(PASSWORD_HASH_FIELD)
                .append(" CHAR(60), ")
                .append(PASSWORD_SALT_FIELD)
                .append(" CHAR(60), ")
                .append(USERS_PHOTO_FIELD)
                .append(" BYTEA)");


        StringBuilder sql_accounts_table;
        sql_accounts_table = new StringBuilder()
                .append("CREATE TABLE ")
                .append(ACCOUNTS_TABLE_NAME)
                .append("(")
                .append(ACCOUNTS_ID_FIELD)
                .append(" INTEGER PRIMARY KEY AUTOINCREMENT, ")
                .append(ACCOUNTS_TITLE_FIELD)
                .append(" VARCHAR, ")
                .append(FK_USERS_ID_FIELD)
                .append(" INT)");


        StringBuilder sql_goals_table;
        sql_goals_table = new StringBuilder()
                .append("CREATE TABLE ")
                .append(GOALS_TABLE_NAME)
                .append("(")
                .append(GOALS_ID_FIELD)
                .append(" INTEGER PRIMARY KEY AUTOINCREMENT, ")
                .append(GOALS_TITLE_FIELD)
                .append(" VARCHAR, ")
                .append(GOALS_DESCRIPTION_FIELD)
                .append(" TEXT, ")
                .append(GOALS_AMOUNT_TO_COLLECT_FIELD)
                .append(" DECIMAL, ")
                .append(FK_ACCOUNTS_ID_FIELD)
                .append(" INT)");


        StringBuilder sql_transactions_table;
        sql_transactions_table = new StringBuilder()
                .append("CREATE TABLE ")
                .append(TRANSACTIONS_TABLE_NAME)
                .append("(")
                .append(TRANSACTIONS_ID_FIELD)
                .append(" INTEGER PRIMARY KEY AUTOINCREMENT, ")
                .append(TRANSACTIONS_TYPE_FIELD)
                .append(" INT, ")
                .append(FK_ACCOUNTS_ID_FROM_FIELD)
                .append(" INT, ")
                .append(FK_ACCOUNTS_ID_TO_FIELD)
                .append(" INT, ")
                .append(TRANSACTIONS_DESCRIPTION_FIELD)
                .append(" TEXT, ")
                .append(TRANSACTIONS_SUM_FIELD)
                .append(" DECIMAL, ")
                .append(TRANSACTIONS_DATE_FIELD)
                .append(" DATE)");

        db.execSQL(sql_users_table.toString());
        db.execSQL(sql_accounts_table.toString());
        db.execSQL(sql_goals_table.toString());
        db.execSQL(sql_transactions_table.toString());
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }
}







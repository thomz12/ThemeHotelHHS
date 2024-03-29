﻿namespace HotelLauncher
{
    public partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.lbl_HTETimespan = new System.Windows.Forms.Label();
            this.lbl_ElevatorSpeed = new System.Windows.Forms.Label();
            this.lbl_WalkingSpeed = new System.Windows.Forms.Label();
            this.num_HTETimespan = new System.Windows.Forms.NumericUpDown();
            this.tt_Settings = new System.Windows.Forms.ToolTip(this.components);
            this.num_ElevatorSpeed = new System.Windows.Forms.NumericUpDown();
            this.num_WalkingSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_FilmDuration = new System.Windows.Forms.NumericUpDown();
            this.lbl_FilmDuration = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_CleaningDuration = new System.Windows.Forms.Label();
            this.num_CleaningDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_Layout = new System.Windows.Forms.TextBox();
            this.lbl_Survivability = new System.Windows.Forms.Label();
            this.num_Survivability = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.num_StairsWeight = new System.Windows.Forms.NumericUpDown();
            this.lbl_StaircaseWeight = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_01 = new System.Windows.Forms.Label();
            this.lbl_ReceptionistWorkLength = new System.Windows.Forms.Label();
            this.num_ReceptionistWorkSpeed = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_NumberOfCleaners = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.num_EatingDuration = new System.Windows.Forms.NumericUpDown();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_SelectLayout = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SelectTexturepack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_HTETimespan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ElevatorSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_WalkingSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FilmDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_CleaningDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Survivability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_StairsWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ReceptionistWorkSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfCleaners)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_EatingDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_HTETimespan
            // 
            this.lbl_HTETimespan.Location = new System.Drawing.Point(9, 9);
            this.lbl_HTETimespan.Name = "lbl_HTETimespan";
            this.lbl_HTETimespan.Size = new System.Drawing.Size(157, 19);
            this.lbl_HTETimespan.TabIndex = 0;
            this.lbl_HTETimespan.Text = "Simulation Speed";
            this.lbl_HTETimespan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_HTETimespan, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation. The" +
        " higher the number, the faster the simulation plays.");
            // 
            // lbl_ElevatorSpeed
            // 
            this.lbl_ElevatorSpeed.Location = new System.Drawing.Point(6, 35);
            this.lbl_ElevatorSpeed.Name = "lbl_ElevatorSpeed";
            this.lbl_ElevatorSpeed.Size = new System.Drawing.Size(160, 19);
            this.lbl_ElevatorSpeed.TabIndex = 1;
            this.lbl_ElevatorSpeed.Text = "Elevator Speed";
            this.lbl_ElevatorSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_ElevatorSpeed, "This sets the speed of the elevator in unit / HTE.");
            // 
            // lbl_WalkingSpeed
            // 
            this.lbl_WalkingSpeed.Location = new System.Drawing.Point(9, 61);
            this.lbl_WalkingSpeed.Name = "lbl_WalkingSpeed";
            this.lbl_WalkingSpeed.Size = new System.Drawing.Size(157, 19);
            this.lbl_WalkingSpeed.TabIndex = 2;
            this.lbl_WalkingSpeed.Text = "Walking Speed";
            this.lbl_WalkingSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_WalkingSpeed, "This sets the walking speed of the people in the hotel in unit/HTE.");
            // 
            // num_HTETimespan
            // 
            this.num_HTETimespan.DecimalPlaces = 1;
            this.num_HTETimespan.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_HTETimespan.Location = new System.Drawing.Point(172, 8);
            this.num_HTETimespan.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_HTETimespan.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.num_HTETimespan.Name = "num_HTETimespan";
            this.num_HTETimespan.Size = new System.Drawing.Size(69, 20);
            this.num_HTETimespan.TabIndex = 0;
            this.tt_Settings.SetToolTip(this.num_HTETimespan, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation. The" +
        " higher the number, the faster the simulation plays.");
            this.num_HTETimespan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_ElevatorSpeed
            // 
            this.num_ElevatorSpeed.DecimalPlaces = 1;
            this.num_ElevatorSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_ElevatorSpeed.Location = new System.Drawing.Point(172, 34);
            this.num_ElevatorSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_ElevatorSpeed.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.num_ElevatorSpeed.Name = "num_ElevatorSpeed";
            this.num_ElevatorSpeed.Size = new System.Drawing.Size(69, 20);
            this.num_ElevatorSpeed.TabIndex = 1;
            this.tt_Settings.SetToolTip(this.num_ElevatorSpeed, "This sets the speed of the elevator in unit / HTE.");
            this.num_ElevatorSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_WalkingSpeed
            // 
            this.num_WalkingSpeed.DecimalPlaces = 1;
            this.num_WalkingSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_WalkingSpeed.Location = new System.Drawing.Point(172, 60);
            this.num_WalkingSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_WalkingSpeed.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            this.num_WalkingSpeed.Name = "num_WalkingSpeed";
            this.num_WalkingSpeed.Size = new System.Drawing.Size(69, 20);
            this.num_WalkingSpeed.TabIndex = 2;
            this.tt_Settings.SetToolTip(this.num_WalkingSpeed, "This sets the walking speed of the people in the hotel in unit/HTE.");
            this.num_WalkingSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "units/HTE";
            this.tt_Settings.SetToolTip(this.label1, "A unit is a single grid square (a 1x1 room) in the hotel. A HTE (Hotel Tijds Eenh" +
        "eid) is a unit of time that is used in the simulation. ");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "units/HTE";
            this.tt_Settings.SetToolTip(this.label2, "A unit is a single grid square (a 1x1 room) in the hotel. A HTE (Hotel Tijds Eenh" +
        "eid) is a unit of time that is used in the simulation. ");
            // 
            // num_FilmDuration
            // 
            this.num_FilmDuration.Location = new System.Drawing.Point(172, 86);
            this.num_FilmDuration.Maximum = new decimal(new int[] {
            1337,
            0,
            0,
            0});
            this.num_FilmDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_FilmDuration.Name = "num_FilmDuration";
            this.num_FilmDuration.Size = new System.Drawing.Size(70, 20);
            this.num_FilmDuration.TabIndex = 3;
            this.tt_Settings.SetToolTip(this.num_FilmDuration, "This sets the length of the movie shown in the cinema.");
            this.num_FilmDuration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lbl_FilmDuration
            // 
            this.lbl_FilmDuration.Location = new System.Drawing.Point(9, 85);
            this.lbl_FilmDuration.Name = "lbl_FilmDuration";
            this.lbl_FilmDuration.Size = new System.Drawing.Size(157, 19);
            this.lbl_FilmDuration.TabIndex = 10;
            this.lbl_FilmDuration.Text = "Film Duration";
            this.lbl_FilmDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_FilmDuration, "This sets the length of the movie shown in the cinema.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label3, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // lbl_CleaningDuration
            // 
            this.lbl_CleaningDuration.Location = new System.Drawing.Point(8, 134);
            this.lbl_CleaningDuration.Name = "lbl_CleaningDuration";
            this.lbl_CleaningDuration.Size = new System.Drawing.Size(157, 19);
            this.lbl_CleaningDuration.TabIndex = 13;
            this.lbl_CleaningDuration.Text = "Cleaning Duration";
            this.lbl_CleaningDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_CleaningDuration, "This sets the duration it takes for a room to be cleaned.");
            // 
            // num_CleaningDuration
            // 
            this.num_CleaningDuration.Location = new System.Drawing.Point(171, 135);
            this.num_CleaningDuration.Maximum = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.num_CleaningDuration.Name = "num_CleaningDuration";
            this.num_CleaningDuration.Size = new System.Drawing.Size(70, 20);
            this.num_CleaningDuration.TabIndex = 5;
            this.tt_Settings.SetToolTip(this.num_CleaningDuration, "This sets the duration it takes for a room to be cleaned.");
            this.num_CleaningDuration.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label5, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "Layout";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.label6, "Choose a layout file for the simulator to display.");
            // 
            // tb_Layout
            // 
            this.tb_Layout.Location = new System.Drawing.Point(84, 291);
            this.tb_Layout.Name = "tb_Layout";
            this.tb_Layout.ReadOnly = true;
            this.tb_Layout.Size = new System.Drawing.Size(291, 20);
            this.tb_Layout.TabIndex = 9;
            this.tb_Layout.TabStop = false;
            this.tt_Settings.SetToolTip(this.tb_Layout, "Choose a layout file for the simulator to display.");
            // 
            // lbl_Survivability
            // 
            this.lbl_Survivability.Location = new System.Drawing.Point(13, 160);
            this.lbl_Survivability.Name = "lbl_Survivability";
            this.lbl_Survivability.Size = new System.Drawing.Size(154, 19);
            this.lbl_Survivability.TabIndex = 17;
            this.lbl_Survivability.Text = "Survivability";
            this.lbl_Survivability.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_Survivability, "This sets the amount of time guests can wait for the elevator. Guests die when th" +
        "ey wait longer for the elevator.");
            // 
            // num_Survivability
            // 
            this.num_Survivability.Location = new System.Drawing.Point(172, 161);
            this.num_Survivability.Maximum = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.num_Survivability.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_Survivability.Name = "num_Survivability";
            this.num_Survivability.Size = new System.Drawing.Size(70, 20);
            this.num_Survivability.TabIndex = 6;
            this.tt_Settings.SetToolTip(this.num_Survivability, "This sets the amount of time guests can wait for the elevator. Guests die when th" +
        "ey wait longer for the elevator.");
            this.num_Survivability.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(248, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label8, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // num_StairsWeight
            // 
            this.num_StairsWeight.DecimalPlaces = 1;
            this.num_StairsWeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.num_StairsWeight.Location = new System.Drawing.Point(172, 187);
            this.num_StairsWeight.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_StairsWeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.num_StairsWeight.Name = "num_StairsWeight";
            this.num_StairsWeight.Size = new System.Drawing.Size(70, 20);
            this.num_StairsWeight.TabIndex = 7;
            this.tt_Settings.SetToolTip(this.num_StairsWeight, "This sets the difficulty to go up or down stairs. The higher the number the less " +
        "likely guests are to take the stairs.");
            this.num_StairsWeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lbl_StaircaseWeight
            // 
            this.lbl_StaircaseWeight.Location = new System.Drawing.Point(13, 186);
            this.lbl_StaircaseWeight.Name = "lbl_StaircaseWeight";
            this.lbl_StaircaseWeight.Size = new System.Drawing.Size(154, 19);
            this.lbl_StaircaseWeight.TabIndex = 21;
            this.lbl_StaircaseWeight.Text = "Staircase Weight";
            this.lbl_StaircaseWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_StaircaseWeight, "This sets the difficulty to go up or down stairs. The higher the number the less " +
        "likely guests are to take the stairs.");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(248, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label10, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // lbl_01
            // 
            this.lbl_01.AutoSize = true;
            this.lbl_01.Location = new System.Drawing.Point(247, 12);
            this.lbl_01.Name = "lbl_01";
            this.lbl_01.Size = new System.Drawing.Size(80, 13);
            this.lbl_01.TabIndex = 6;
            this.lbl_01.Text = "HTE / seconds";
            this.tt_Settings.SetToolTip(this.lbl_01, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // lbl_ReceptionistWorkLength
            // 
            this.lbl_ReceptionistWorkLength.Location = new System.Drawing.Point(13, 212);
            this.lbl_ReceptionistWorkLength.Name = "lbl_ReceptionistWorkLength";
            this.lbl_ReceptionistWorkLength.Size = new System.Drawing.Size(154, 19);
            this.lbl_ReceptionistWorkLength.TabIndex = 23;
            this.lbl_ReceptionistWorkLength.Text = "Check In/Out Length";
            this.lbl_ReceptionistWorkLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.lbl_ReceptionistWorkLength, "This sets the time it takes for the receptionist to check people in and out.");
            // 
            // num_ReceptionistWorkSpeed
            // 
            this.num_ReceptionistWorkSpeed.DecimalPlaces = 1;
            this.num_ReceptionistWorkSpeed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.num_ReceptionistWorkSpeed.Location = new System.Drawing.Point(172, 213);
            this.num_ReceptionistWorkSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_ReceptionistWorkSpeed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.num_ReceptionistWorkSpeed.Name = "num_ReceptionistWorkSpeed";
            this.num_ReceptionistWorkSpeed.Size = new System.Drawing.Size(70, 20);
            this.num_ReceptionistWorkSpeed.TabIndex = 8;
            this.tt_Settings.SetToolTip(this.num_ReceptionistWorkSpeed, "This sets the time it takes for the receptionist to check people in and out.");
            this.num_ReceptionistWorkSpeed.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label7, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "Number of Cleaners";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.label4, "This sets the amount of cleaners that are in the hotel.");
            // 
            // num_NumberOfCleaners
            // 
            this.num_NumberOfCleaners.Location = new System.Drawing.Point(172, 239);
            this.num_NumberOfCleaners.Maximum = new decimal(new int[] {
            42,
            0,
            0,
            0});
            this.num_NumberOfCleaners.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumberOfCleaners.Name = "num_NumberOfCleaners";
            this.num_NumberOfCleaners.Size = new System.Drawing.Size(70, 20);
            this.num_NumberOfCleaners.TabIndex = 9;
            this.tt_Settings.SetToolTip(this.num_NumberOfCleaners, "This sets the amount of cleaners that are in the hotel.");
            this.num_NumberOfCleaners.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(246, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "HTE(s)";
            this.tt_Settings.SetToolTip(this.label9, "A HTE (Hotel Tijds Eenheid) is a unit of time that is used in the simulation.");
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 19);
            this.label11.TabIndex = 32;
            this.label11.Text = "Eating Duration";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tt_Settings.SetToolTip(this.label11, "This sets the amount of time people stay in restaurants.");
            // 
            // num_EatingDuration
            // 
            this.num_EatingDuration.Location = new System.Drawing.Point(171, 111);
            this.num_EatingDuration.Maximum = new decimal(new int[] {
            1337,
            0,
            0,
            0});
            this.num_EatingDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_EatingDuration.Name = "num_EatingDuration";
            this.num_EatingDuration.Size = new System.Drawing.Size(70, 20);
            this.num_EatingDuration.TabIndex = 4;
            this.tt_Settings.SetToolTip(this.num_EatingDuration, "This sets the amount of time people stay in restaurants.");
            this.num_EatingDuration.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(107, 317);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(90, 32);
            this.btn_Save.TabIndex = 12;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_SelectLayout
            // 
            this.btn_SelectLayout.Location = new System.Drawing.Point(381, 291);
            this.btn_SelectLayout.Name = "btn_SelectLayout";
            this.btn_SelectLayout.Size = new System.Drawing.Size(26, 20);
            this.btn_SelectLayout.TabIndex = 11;
            this.btn_SelectLayout.Text = "...";
            this.btn_SelectLayout.UseVisualStyleBackColor = true;
            this.btn_SelectLayout.Click += new System.EventHandler(this.btn_SelectLayout_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(203, 317);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(90, 32);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SelectTexturepack
            // 
            this.btn_SelectTexturepack.Location = new System.Drawing.Point(148, 265);
            this.btn_SelectTexturepack.Name = "btn_SelectTexturepack";
            this.btn_SelectTexturepack.Size = new System.Drawing.Size(115, 20);
            this.btn_SelectTexturepack.TabIndex = 10;
            this.btn_SelectTexturepack.Text = "Select Texture Pack";
            this.btn_SelectTexturepack.UseVisualStyleBackColor = true;
            this.btn_SelectTexturepack.Click += new System.EventHandler(this.btn_SelectTexturepack_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(420, 359);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.num_EatingDuration);
            this.Controls.Add(this.btn_SelectTexturepack);
            this.Controls.Add(this.num_NumberOfCleaners);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.num_ReceptionistWorkSpeed);
            this.Controls.Add(this.lbl_ReceptionistWorkLength);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl_StaircaseWeight);
            this.Controls.Add(this.num_StairsWeight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.num_Survivability);
            this.Controls.Add(this.lbl_Survivability);
            this.Controls.Add(this.tb_Layout);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_SelectLayout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.num_CleaningDuration);
            this.Controls.Add(this.lbl_CleaningDuration);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_FilmDuration);
            this.Controls.Add(this.num_FilmDuration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_01);
            this.Controls.Add(this.num_WalkingSpeed);
            this.Controls.Add(this.num_ElevatorSpeed);
            this.Controls.Add(this.num_HTETimespan);
            this.Controls.Add(this.lbl_WalkingSpeed);
            this.Controls.Add(this.lbl_ElevatorSpeed);
            this.Controls.Add(this.lbl_HTETimespan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_HTETimespan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ElevatorSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_WalkingSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FilmDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_CleaningDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Survivability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_StairsWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ReceptionistWorkSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfCleaners)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_EatingDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_HTETimespan;
        private System.Windows.Forms.Label lbl_ElevatorSpeed;
        private System.Windows.Forms.Label lbl_WalkingSpeed;
        private System.Windows.Forms.NumericUpDown num_HTETimespan;
        private System.Windows.Forms.ToolTip tt_Settings;
        private System.Windows.Forms.NumericUpDown num_WalkingSpeed;
        private System.Windows.Forms.Label lbl_01;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_ElevatorSpeed;
        private System.Windows.Forms.NumericUpDown num_FilmDuration;
        private System.Windows.Forms.Label lbl_FilmDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label lbl_CleaningDuration;
        private System.Windows.Forms.NumericUpDown num_CleaningDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_SelectLayout;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox tb_Layout;
        private System.Windows.Forms.Label lbl_Survivability;
        private System.Windows.Forms.NumericUpDown num_Survivability;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_StairsWeight;
        private System.Windows.Forms.Label lbl_StaircaseWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_ReceptionistWorkLength;
        private System.Windows.Forms.NumericUpDown num_ReceptionistWorkSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_NumberOfCleaners;
        private System.Windows.Forms.Button btn_SelectTexturepack;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown num_EatingDuration;
    }
}
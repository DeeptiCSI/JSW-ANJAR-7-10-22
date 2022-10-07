﻿// <auto-generated />
using System;
using CSI_PPMS.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSI_PPMS.Migrations
{
    [DbContext(typeof(PPMSContext))]
    [Migration("20220726111639_columnadded")]
    partial class columnadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CSI_PPMS.Entity.CheckBoxTable", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsMarked")
                        .HasColumnType("bit")
                        .HasColumnName("is_marked");

                    b.Property<bool>("IsPunched")
                        .HasColumnType("bit")
                        .HasColumnName("is_punched");

                    b.HasKey("Id");

                    b.ToTable("check_box_table");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.ColdLevellerRecords", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("length");

                    b.Property<long>("PlateId")
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<string>("SteelGrade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("steel_grade");

                    b.Property<decimal>("Thickness")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("thickness");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("weight");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("width");

                    b.HasKey("Id");

                    b.HasIndex("PlateId");

                    b.ToTable("cold_leveller_records");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.Module", b =>
                {
                    b.Property<long>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("module_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ModuleName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("module_name");

                    b.HasKey("ModuleId");

                    b.ToTable("module");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateDataFromSapColdLeveller", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("grade");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("length");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<string>("Return1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("return1");

                    b.Property<string>("Thickness")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("thickness");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("weight");

                    b.Property<string>("Width")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("width");

                    b.Property<string>("YST")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ys_t");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("plate_data_from_sap_cold_leveller");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateInfoFromSAP", b =>
                {
                    b.Property<long>("PlateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualWeight")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("actual_weight");

                    b.Property<int?>("CharecterCount")
                        .HasColumnType("int")
                        .HasColumnName("charecter_count");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("customer_name");

                    b.Property<string>("CustomerReference")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("customer_reference");

                    b.Property<DateTime?>("DataFromSAPDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_from_sap_date");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("grade");

                    b.Property<string>("GradeDuel")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("grade_duel");

                    b.Property<string>("HeatNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("heat_number");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("length");

                    b.Property<string>("MaterialDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("material_description");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("project_name");

                    b.Property<long?>("PurchaseOrder")
                        .HasColumnType("bigint")
                        .HasColumnName("purchase_order");

                    b.Property<string>("PurchaseOrderNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("purchase_order_number");

                    b.Property<DateTime?>("SentToMarkingTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("sent_to_marking_time");

                    b.Property<DateTime?>("SentToPunchingTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("sent_to_punching_time");

                    b.Property<bool?>("TheoreticalWeight")
                        .HasColumnType("bit")
                        .HasColumnName("theoretical_weight");

                    b.Property<string>("Thickness")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("thickness");

                    b.Property<DateTime?>("UpdateToSAPTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("update_to_sap_time");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<int?>("WeighingMode")
                        .HasColumnType("int")
                        .HasColumnName("weighing_mode");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("weight");

                    b.Property<DateTime?>("WeightReadTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("weight_read_time");

                    b.Property<string>("Width")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("width");

                    b.HasKey("PlateId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("plate_info_from_sap");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateMarkingDataForReceipe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line1");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line2");

                    b.Property<string>("Line3")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line3");

                    b.Property<string>("Line4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line4");

                    b.Property<string>("Line5")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line5");

                    b.Property<string>("Line6")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line6");

                    b.Property<long>("MarkingPosition")
                        .HasColumnType("bigint")
                        .HasColumnName("marking_position");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<long>("PlateId")
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<int?>("PlcAck")
                        .HasColumnType("int")
                        .HasColumnName("plc_ack");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("plate_marking_data_for_receipe");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateMarkingRecord", b =>
                {
                    b.Property<long>("MarkingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("marking_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line1");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line2");

                    b.Property<string>("Line3")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line3");

                    b.Property<string>("Line4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line4");

                    b.Property<string>("Line5")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line5");

                    b.Property<string>("Line6")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line6");

                    b.Property<long?>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<long>("PlateId")
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.HasKey("MarkingId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("PlateId");

                    b.ToTable("plate_marking_record");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlatePunchingDataForReceipe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line1");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line2");

                    b.Property<string>("Line3")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line3");

                    b.Property<string>("Line4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line4");

                    b.Property<string>("Line5")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line5");

                    b.Property<string>("Line6")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line6");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<long>("PlateId")
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<int?>("PlcAck")
                        .HasColumnType("int")
                        .HasColumnName("plc_ack");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("plate_punching_data_for_receipe");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlatePunchingRecord", b =>
                {
                    b.Property<long>("PunchingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Punching_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line1");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line2");

                    b.Property<string>("Line3")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line3");

                    b.Property<string>("Line4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line4");

                    b.Property<string>("Line5")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line5");

                    b.Property<string>("Line6")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("line6");

                    b.Property<long?>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<long>("PlateId")
                        .HasColumnType("bigint")
                        .HasColumnName("plate_id");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plate_number");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.HasKey("PunchingId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("PlateId");

                    b.ToTable("plate_punching_record");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PunchingCycleStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DataRequestACK")
                        .HasColumnType("bigint")
                        .HasColumnName("Data_Request_Ack");

                    b.Property<long>("MarkingCompleteACK")
                        .HasColumnType("bigint")
                        .HasColumnName("Marking_Complete_Ack");

                    b.Property<long>("MarkingStatusACK")
                        .HasColumnType("bigint")
                        .HasColumnName("Marking_Status_Ack");

                    b.Property<int>("PLC_MODE")
                        .HasColumnType("int")
                        .HasColumnName("PLC_MODE");

                    b.Property<long>("StartPunching")
                        .HasColumnType("bigint")
                        .HasColumnName("Start_Punching");

                    b.HasKey("Id");

                    b.ToTable("punching_cycle_status");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("role_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.ToTable("role");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.SapCredentials", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<long?>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<string>("SAPLink")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sap_link");

                    b.Property<string>("SAPPassword")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sap_password");

                    b.Property<string>("SAPUserName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sap_username");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("sap_credentials");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TCPConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<string>("PLCIPAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("plc_ip_address");

                    b.Property<int>("PLCPortNo")
                        .HasColumnType("int")
                        .HasColumnName("plc_port_no");

                    b.Property<string>("TechniforIPAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("technifor_ip_address");

                    b.Property<int>("TechniforPortNo")
                        .HasColumnType("int")
                        .HasColumnName("technifor_port_no");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("tcp_config");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TemplateMaster", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdDate");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("is_default");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<string>("TemplateName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("template_name");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModuleId");

                    b.ToTable("template_master");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TemplateRows", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Row")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("row");

                    b.Property<long>("TemplateMasterId")
                        .HasColumnType("bigint")
                        .HasColumnName("template_master_id");

                    b.HasKey("Id");

                    b.HasIndex("TemplateMasterId");

                    b.ToTable("table_rows");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("user_name");

                    b.HasKey("UserId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.UserRole", b =>
                {
                    b.Property<long>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("user_role_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint")
                        .HasColumnName("module_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("UserRoleId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.ColdLevellerRecords", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.PlateDataFromSapColdLeveller", "PlateDataFromSapColdLeveller")
                        .WithMany("ColdLevellerRecords")
                        .HasForeignKey("PlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlateDataFromSapColdLeveller");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateDataFromSapColdLeveller", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlateDataFromSapColdLeveller")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSI_PPMS.Entity.User", "User")
                        .WithMany("PlateDataFromSapColdLeveller")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateInfoFromSAP", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlateInfoFromSAP")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSI_PPMS.Entity.User", "User")
                        .WithMany("PlateInfoFromSAP")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateMarkingDataForReceipe", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlateMarkingDataForReceipe")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateMarkingRecord", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlateMarkingRecord")
                        .HasForeignKey("ModuleId");

                    b.HasOne("CSI_PPMS.Entity.PlateInfoFromSAP", "PlateInfoFromSAP")
                        .WithMany("PlateMarkingRecord")
                        .HasForeignKey("PlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("PlateInfoFromSAP");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlatePunchingDataForReceipe", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlatePunchingDataForReceipe")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlatePunchingRecord", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("PlatePunchingRecord")
                        .HasForeignKey("ModuleId");

                    b.HasOne("CSI_PPMS.Entity.PlateInfoFromSAP", "PlateInfoFromSAP")
                        .WithMany("PlatePunchingRecord")
                        .HasForeignKey("PlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("PlateInfoFromSAP");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.SapCredentials", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("SapCredentials")
                        .HasForeignKey("ModuleId");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TCPConfig", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("TCPConfig")
                        .HasForeignKey("ModuleId");

                    b.HasOne("CSI_PPMS.Entity.User", "User")
                        .WithMany("TCPConfig")
                        .HasForeignKey("UserId");

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TemplateMaster", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.User", "User")
                        .WithMany("TemplateMaster")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("TemplateMaster")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TemplateRows", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.TemplateMaster", "TemplateMaster")
                        .WithMany("TemplateRows")
                        .HasForeignKey("TemplateMasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TemplateMaster");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.UserRole", b =>
                {
                    b.HasOne("CSI_PPMS.Entity.Module", "Module")
                        .WithMany("UserRole")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSI_PPMS.Entity.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSI_PPMS.Entity.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.Module", b =>
                {
                    b.Navigation("PlateDataFromSapColdLeveller");

                    b.Navigation("PlateInfoFromSAP");

                    b.Navigation("PlateMarkingDataForReceipe");

                    b.Navigation("PlateMarkingRecord");

                    b.Navigation("PlatePunchingDataForReceipe");

                    b.Navigation("PlatePunchingRecord");

                    b.Navigation("SapCredentials");

                    b.Navigation("TCPConfig");

                    b.Navigation("TemplateMaster");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateDataFromSapColdLeveller", b =>
                {
                    b.Navigation("ColdLevellerRecords");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.PlateInfoFromSAP", b =>
                {
                    b.Navigation("PlateMarkingRecord");

                    b.Navigation("PlatePunchingRecord");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.Role", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.TemplateMaster", b =>
                {
                    b.Navigation("TemplateRows");
                });

            modelBuilder.Entity("CSI_PPMS.Entity.User", b =>
                {
                    b.Navigation("PlateDataFromSapColdLeveller");

                    b.Navigation("PlateInfoFromSAP");

                    b.Navigation("TCPConfig");

                    b.Navigation("TemplateMaster");

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}

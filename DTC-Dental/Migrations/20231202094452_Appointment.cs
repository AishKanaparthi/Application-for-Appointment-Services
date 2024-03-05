using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DTC_Dental.Migrations
{
    /// <inheritdoc />
    public partial class Appointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentID", "AppointmentDate", "AppointmentTypeID", "DentistID", "PatientID", "StartTime" },
                values: new object[,]
                {
                    { 101, new DateTime(2023, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 38345, 55746, new TimeSpan(0, 12, 0, 0, 0) },
                    { 102, new DateTime(2023, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 43377, 34437, new TimeSpan(0, 8, 0, 0, 0) },
                    { 103, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 13340, 74739, new TimeSpan(0, 10, 30, 0, 0) },
                    { 104, new DateTime(2023, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 18631, 36956, new TimeSpan(0, 15, 30, 0, 0) },
                    { 105, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13340, 88306, new TimeSpan(0, 11, 0, 0, 0) },
                    { 106, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 17381, 94954, new TimeSpan(0, 12, 30, 0, 0) },
                    { 107, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 18631, 41469, new TimeSpan(0, 9, 0, 0, 0) },
                    { 108, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 18631, 78740, new TimeSpan(0, 10, 0, 0, 0) },
                    { 109, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 38345, 89349, new TimeSpan(0, 12, 0, 0, 0) },
                    { 110, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 43377, 56397, new TimeSpan(0, 13, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentID",
                keyValue: 110);
        }
    }
}

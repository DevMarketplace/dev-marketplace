using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InsertProjectRelatedStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ProjectStatus (Status) VALUES ('Draft')");
            migrationBuilder.Sql("INSERT INTO ProjectStatus (Status) VALUES ('Open')");
            migrationBuilder.Sql("INSERT INTO ProjectStatus (Status) VALUES ('Closed')");

            //Please refer to the order when building the state machine
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('BiddingOn')"); //1
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('BiddingWon')"); //2
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('Development')"); //3
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('WaitingApproval')"); //4
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('Approved')"); //5
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('PaymentPending')"); //6
            migrationBuilder.Sql("INSERT INTO CardStatus (Status) VALUES ('Complete')"); //7
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ProjectStatus");
            migrationBuilder.Sql("DELETE FROM CardStatus");
        }
    }
}

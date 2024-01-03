using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string SpInsertInvoice = @"CREATE OR ALTER PROCEDURE dbo.SpInsertInvoice 
    @CreatedDate datetime2(7),
    @PassengerName nvarchar(max),  
    @Address nvarchar(max),  
    @ContactNo nvarchar(max),
	@ImagePath nvarchar(max),
	@CardHolder bit

AS
INSERT INTO [dbo].[Invoices]
           ([CreatedDate]
           ,[PassengerName]
           ,[Address]
           ,[ContactNo]
		   ,[ImagePath]
		   ,[CardHolder])

     VALUES
           (@CreatedDate, @PassengerName, @Address, @ContactNo,@ImagePath,@CardHolder)

		   return @@identity 
GO";

            migrationBuilder.Sql(SpInsertInvoice);


            string SpInsertTicketInfo = @"CREATE OR ALTER PROCEDURE dbo.SpInsertTicketInfo 
	@InvoiceId int,
    @TicketId int,
    @Quantity int

	as

INSERT INTO [dbo].[TicketInfos]
           ([TicketId]
           ,[Quantity]
           ,[InvoiceId])
     VALUES
           (@TicketId, @Quantity, @InvoiceId )

		   return @@rowcount
GO";

            migrationBuilder.Sql(SpInsertTicketInfo);

            string SpUpdateInvoice = @"CREATE OR ALTER PROCEDURE dbo.SpUpdateInvoice 
    @InvoiceId int,
    @CreatedDate datetime2(7),
    @PassengerName nvarchar(max),  
    @Address nvarchar(max),  
    @ContactNo nvarchar(max),
	@ImagePath nvarchar(max),
	@CardHolder bit

AS

UPDATE [dbo].[Invoices]
   SET [CreatedDate] = @CreatedDate
      ,[PassengerName] = @PassengerName
      ,[Address] = @Address
      ,[ContactNo] = @ContactNo
	  ,[ImagePath]= @ImagePath
	  ,[CardHolder]=@CardHolder
	  where Id = @InvoiceId

	  delete from TicketInfos where InvoiceId = @InvoiceId

	  return @@rowcount
GO";
            migrationBuilder.Sql(SpUpdateInvoice);


            string SpDeleteInvoice = @"CREATE OR ALTER PROCEDURE dbo.SpDeleteInvoice 
    @InvoiceId int
AS
	  delete from TicketInfos where InvoiceId = @InvoiceId
	   delete from [Invoices] where Id = @InvoiceId

	  return @@rowcount
GO";

            migrationBuilder.Sql(SpDeleteInvoice);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc SpInsertInvoice");
            migrationBuilder.Sql("drop proc SpInsertTicketInfo");
            migrationBuilder.Sql("drop proc SpUpdateInvoice");
            migrationBuilder.Sql("drop proc SpDeleteInvoice");

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suppfly.Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedNamingConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_users_ApprovedByUserId",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "FK_company_approval_requests_companies_CompanyId",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_company_approval_requests_users_RequestedByUserId",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_company_approval_requests_users_ReviewedByUserId",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_users_companies_CompanyId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company_approval_requests",
                table: "company_approval_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_companies",
                table: "companies");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "users",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiry",
                table: "users",
                newName: "refresh_token_expiry");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "users",
                newName: "refresh_token");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                table: "users",
                newName: "last_login_at");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "users",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "users",
                newName: "ix_users_email");

            migrationBuilder.RenameIndex(
                name: "IX_users_CompanyId",
                table: "users",
                newName: "ix_users_company_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "company_approval_requests",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "company_approval_requests",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "company_approval_requests",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "company_approval_requests",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ReviewedByUserId",
                table: "company_approval_requests",
                newName: "reviewed_by_user_id");

            migrationBuilder.RenameColumn(
                name: "ReviewedAt",
                table: "company_approval_requests",
                newName: "reviewed_at");

            migrationBuilder.RenameColumn(
                name: "RequestedByUserId",
                table: "company_approval_requests",
                newName: "requested_by_user_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "company_approval_requests",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "company_approval_requests",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_approval_requests_ReviewedByUserId",
                table: "company_approval_requests",
                newName: "ix_company_approval_requests_reviewed_by_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_approval_requests_RequestedByUserId",
                table: "company_approval_requests",
                newName: "ix_company_approval_requests_requested_by_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_company_approval_requests_CompanyId",
                table: "company_approval_requests",
                newName: "ix_company_approval_requests_company_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "companies",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Tier",
                table: "companies",
                newName: "tier");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "companies",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "companies",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "companies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "companies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "companies",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "companies",
                newName: "tax_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "companies",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ApprovedByUserId",
                table: "companies",
                newName: "approved_by_user_id");

            migrationBuilder.RenameColumn(
                name: "ApprovedAt",
                table: "companies",
                newName: "approved_at");

            migrationBuilder.RenameIndex(
                name: "IX_companies_Slug",
                table: "companies",
                newName: "ix_companies_slug");

            migrationBuilder.RenameIndex(
                name: "IX_companies_ApprovedByUserId",
                table: "companies",
                newName: "ix_companies_approved_by_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_company_approval_requests",
                table: "company_approval_requests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_companies",
                table: "companies",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_companies_users_approved_by_user_id",
                table: "companies",
                column: "approved_by_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_company_approval_requests_companies_company_id",
                table: "company_approval_requests",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_company_approval_requests_users_requested_by_user_id",
                table: "company_approval_requests",
                column: "requested_by_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_company_approval_requests_users_reviewed_by_user_id",
                table: "company_approval_requests",
                column: "reviewed_by_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_users_companies_company_id",
                table: "users",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_companies_users_approved_by_user_id",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_company_approval_requests_companies_company_id",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_company_approval_requests_users_requested_by_user_id",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_company_approval_requests_users_reviewed_by_user_id",
                table: "company_approval_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_users_companies_company_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_company_approval_requests",
                table: "company_approval_requests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_companies",
                table: "companies");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "users",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "refresh_token_expiry",
                table: "users",
                newName: "RefreshTokenExpiry");

            migrationBuilder.RenameColumn(
                name: "refresh_token",
                table: "users",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "last_login_at",
                table: "users",
                newName: "LastLoginAt");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "users",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "ix_users_email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameIndex(
                name: "ix_users_company_id",
                table: "users",
                newName: "IX_users_CompanyId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "company_approval_requests",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "company_approval_requests",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "company_approval_requests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "company_approval_requests",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "reviewed_by_user_id",
                table: "company_approval_requests",
                newName: "ReviewedByUserId");

            migrationBuilder.RenameColumn(
                name: "reviewed_at",
                table: "company_approval_requests",
                newName: "ReviewedAt");

            migrationBuilder.RenameColumn(
                name: "requested_by_user_id",
                table: "company_approval_requests",
                newName: "RequestedByUserId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "company_approval_requests",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "company_approval_requests",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "ix_company_approval_requests_reviewed_by_user_id",
                table: "company_approval_requests",
                newName: "IX_company_approval_requests_ReviewedByUserId");

            migrationBuilder.RenameIndex(
                name: "ix_company_approval_requests_requested_by_user_id",
                table: "company_approval_requests",
                newName: "IX_company_approval_requests_RequestedByUserId");

            migrationBuilder.RenameIndex(
                name: "ix_company_approval_requests_company_id",
                table: "company_approval_requests",
                newName: "IX_company_approval_requests_CompanyId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "companies",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "tier",
                table: "companies",
                newName: "Tier");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "companies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "companies",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "companies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "companies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "companies",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "tax_id",
                table: "companies",
                newName: "TaxId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "companies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "approved_by_user_id",
                table: "companies",
                newName: "ApprovedByUserId");

            migrationBuilder.RenameColumn(
                name: "approved_at",
                table: "companies",
                newName: "ApprovedAt");

            migrationBuilder.RenameIndex(
                name: "ix_companies_slug",
                table: "companies",
                newName: "IX_companies_Slug");

            migrationBuilder.RenameIndex(
                name: "ix_companies_approved_by_user_id",
                table: "companies",
                newName: "IX_companies_ApprovedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company_approval_requests",
                table: "company_approval_requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_companies",
                table: "companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_companies_users_ApprovedByUserId",
                table: "companies",
                column: "ApprovedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_company_approval_requests_companies_CompanyId",
                table: "company_approval_requests",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_approval_requests_users_RequestedByUserId",
                table: "company_approval_requests",
                column: "RequestedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_company_approval_requests_users_ReviewedByUserId",
                table: "company_approval_requests",
                column: "ReviewedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_users_companies_CompanyId",
                table: "users",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using FluentMigrator;

namespace GymInnowise.UserService.Persistence.Migrations
{
    [Migration(202430080204)]
    public class InitialCreate : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("ClientProfiles").Exists())
            {
                UpClientProfiles();
            }

            if (!Schema.Table("CoachProfiles").Exists())
            {
                UpCoachProfiles();
            }

            if (!Schema.Table("PersonalGoals").Exists())
            {
                UpPersonalGoals();
            }
        }

        private void UpClientProfiles()
        {
            Create.Table("ClientProfiles")
                .WithColumn("AccountId").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(50).Nullable()
                .WithColumn("LastName").AsString(50).Nullable()
                .WithColumn("DateOfBirth").AsDateTimeOffset().Nullable()
                .WithColumn("Gender").AsString(50).Nullable()
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("AccountStatus").AsString(50).Nullable()
                .WithColumn("StatusNotes").AsString(1000).Nullable()
                .WithColumn("ExpectedReturnDate").AsDateTimeOffset().Nullable()
                .WithColumn("Tags").AsString().Nullable();
        }

        private void UpCoachProfiles()
        {
            Create.Table("CoachProfiles")
                .WithColumn("AccountId").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(50).Nullable()
                .WithColumn("LastName").AsString(50).Nullable()
                .WithColumn("DateOfBirth").AsDateTimeOffset().Nullable()
                .WithColumn("Gender").AsString(50).Nullable()
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("HiredAt").AsDateTimeOffset().Nullable()
                .WithColumn("CostPerHour").AsDecimal().Nullable()
                .WithColumn("AccountStatus").AsString(50).Nullable()
                .WithColumn("StatusNotes").AsString(1000).Nullable()
                .WithColumn("ExpectedReturnDate").AsDateTimeOffset().Nullable()
                .WithColumn("CoachStatus").AsString(50).Nullable()
                .WithColumn("Tags").AsString().Nullable();
        }

        private void UpPersonalGoals()
        {
            Create.Table("PersonalGoals")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Owner").AsGuid().NotNullable()
                .WithColumn("Goal").AsString(200).NotNullable()
                .WithColumn("SupervisorCoach").AsGuid().Nullable()
                .WithColumn("Status").AsString(50).Nullable()
                .WithColumn("StartDate").AsDateTimeOffset().NotNullable()
                .WithColumn("DeadLine").AsDateTimeOffset().Nullable();
        }

        public override void Down()
        {
            Delete.Table("ClientProfiles");
            Delete.Table("CoachProfiles");
            Delete.Table("PersonalGoals");
        }
    }
}

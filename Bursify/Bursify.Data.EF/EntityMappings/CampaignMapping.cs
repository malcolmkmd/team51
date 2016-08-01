﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Bursify.Data.EF.CampaignUser;

namespace Bursify.Data.EF.EntityMappings
{
    public class CampaignMapping : EntityTypeConfiguration<Campaign>
    {
        public CampaignMapping()
        {
            this.ToTable("Campaign", "dbo");

            this.HasKey(x => x.ID);

            this.Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.StudentId)
                .IsRequired();

            this.Property(x => x.CampaignName)
                .HasMaxLength(200)
                .IsRequired();

            this.Property(x => x.Tagline)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(x => x.Location)
                .HasMaxLength(500)
                .IsRequired();

            this.Property(x => x.Description)
                .IsRequired();

            this.Property(x => x.AmountRequired)
                .IsRequired();

            this.Property(x => x.CampaignType)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(x => x.VideoPath)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(x => x.PicturePath)
                .HasMaxLength(200)
                .IsOptional();

            this.Property(x => x.StartDate)
                .IsRequired();

            this.Property(x => x.EndDate)
                .IsRequired();

            this.Property(x => x.AmountContributed)
                .IsRequired();

            this.Property(x => x.FundUsage)
                .IsRequired();

            this.Property(x => x.ReasonsToSupport)
                .IsOptional();

            this.Property(x => x.NumberOfUpVotes)
                .IsOptional();

            this.HasMany(x => x.CampaignSponsors);

            this.HasRequired(x => x.Student)
                .WithMany(c => c.Campaigns)
                .HasForeignKey(s => s.StudentId);

            this.HasRequired(x => x.Account)
                .WithRequiredPrincipal(x => x.Campaign);
        }
    }
}
﻿// <auto-generated />
using System;
using Lykke.Service.Staking.MsSqlRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lykke.Service.Staking.MsSqlRepositories.Migrations
{
    [DbContext(typeof(StakingContext))]
    partial class StakingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("staking")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lykke.Service.Staking.MsSqlRepositories.Entities.ReferralStakeEntity", b =>
                {
                    b.Property<string>("ReferralId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("referral_id");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnName("amount");

                    b.Property<string>("CampaignId")
                        .IsRequired()
                        .HasColumnName("campaign_id");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnName("customer_id");

                    b.Property<decimal>("ExpirationBurnRatio")
                        .HasColumnName("expiration_burn_ratio");

                    b.Property<bool>("IsWarningSent")
                        .HasColumnName("is_warning_sent");

                    b.Property<decimal?>("ReleaseBurnRatio")
                        .HasColumnName("release_burn_ratio");

                    b.Property<int>("StakingPeriodInDays")
                        .HasColumnName("staking_period_in_days");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp");

                    b.Property<int>("WarningPeriodInDays")
                        .HasColumnName("warning_period_in_days");

                    b.HasKey("ReferralId");

                    b.ToTable("referral_stakes");
                });

            modelBuilder.Entity("Lykke.Service.Staking.MsSqlRepositories.Entities.StakesBlockchainEntity", b =>
                {
                    b.Property<string>("StakeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("stake_id");

                    b.Property<string>("LastOperationId")
                        .IsRequired()
                        .HasColumnName("last_operation_id");

                    b.HasKey("StakeId");

                    b.ToTable("stakes_blockchain_info");
                });
#pragma warning restore 612, 618
        }
    }
}

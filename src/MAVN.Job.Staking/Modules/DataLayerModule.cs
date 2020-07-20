﻿using Autofac;
using JetBrains.Annotations;
using Lykke.Job.Staking.Settings;
using Lykke.Job.Staking.Settings.JobSettings;
using MAVN.Service.Staking.Domain.Repositories;
using MAVN.Service.Staking.MsSqlRepositories;
using MAVN.Service.Staking.MsSqlRepositories.Repositories;
using Lykke.SettingsReader;
using MAVN.Persistence.PostgreSQL.Legacy;

namespace Lykke.Job.Staking.Modules
{
    [UsedImplicitly]
    public class DataLayerModule : Module
    {
        private readonly DbSettings _settings;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _settings = appSettings.CurrentValue.StakingJob.Db;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(
                _settings.DataConnString,
                connString => new StakingContext(connString, false),
                dbConn => new StakingContext(dbConn));

            builder.RegisterType<ReferralStakesRepository>()
                .As<IReferralStakesRepository>()
                .SingleInstance();

            builder.RegisterType<StakesBlockchainDataRepository>()
                .As<IStakesBlockchainDataRepository>()
                .SingleInstance();
        }
    }
}

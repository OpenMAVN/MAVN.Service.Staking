﻿using System.Threading.Tasks;
using Lykke.Service.Staking.Domain.Enums;

namespace Lykke.Service.Staking.Domain.Services
{
    public interface IReferralStakesStatusUpdater
    {
        Task<ReferralStakesStatusUpdateErrorCode> TokensReservationSucceedAsync(string referralId);

        Task<ReferralStakesStatusUpdateErrorCode> TokensReservationFailAsync(string referralId);

        Task<ReferralStakesStatusUpdateErrorCode> TokensBurnAndReleaseAsync(string referralId, decimal? releaseBurnRatio = null);

        Task<ReferralStakesStatusUpdateErrorCode> TokensBurnAndReleaseSucceedAsync(string referralId);

        Task<ReferralStakesStatusUpdateErrorCode> TokensBurnAndReleaseFailAsync(string referralId);
    }
}

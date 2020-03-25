﻿using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.Service.Staking.Domain.Enums;
using Lykke.Service.Staking.Domain.Models;
using Lykke.Service.Staking.Domain.Repositories;
using Lykke.Service.Staking.Domain.Services;
using Lykke.Service.Staking.DomainServices.RabbitMq.Handlers;
using Lykke.Service.Staking.DomainServices.Services;
using Lykke.Service.Staking.MsSqlRepositories.Entities;
using Moq;
using Xunit;

namespace Lykke.Service.Staking.Tests
{
    public class TransactionSucceededEventHandlerTests
    {
        private const string FakeOperationId = "opId";
        private const string FakeReferralId = "refId";

        private readonly Mock<IReferralStakesRepository> _referralStakesRepoMock = new Mock<IReferralStakesRepository>();
        private readonly Mock<IStakesBlockchainDataRepository> _stakeBlockchainDataRepoMock = new Mock<IStakesBlockchainDataRepository>();
        private readonly Mock<IReferralStakesStatusUpdater> _referralStakesStatusUpdaterMock = new Mock<IReferralStakesStatusUpdater>(MockBehavior.Strict);


        [Fact]
        public async Task HandleAsync_OperationNotFoundInBlockchainDataRepo_ReferralStakesRepoNotCalled()
        {
            _stakeBlockchainDataRepoMock.Setup(x => x.GetByOperationIdAsync(FakeOperationId))
                .ReturnsAsync((IStakesBlockchainData) null);

            var sut = CreateSutInstance();

            await sut.HandleAsync(FakeOperationId);

            _referralStakesRepoMock.Verify(x => x.GetByReferralIdAsync(It.IsAny<string>(), null), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_OperationIsMissingInReferralStakesRepo_StatusUpdaterNotCalled()
        {
            _stakeBlockchainDataRepoMock.Setup(x => x.GetByOperationIdAsync(FakeOperationId))
                .ReturnsAsync(new StakesBlockchainEntity{StakeId = FakeReferralId});

            _referralStakesRepoMock.Setup(x => x.GetByReferralIdAsync(FakeReferralId, null))
                .ReturnsAsync((ReferralStakeModel) null);

            var sut = CreateSutInstance();

            await sut.HandleAsync(FakeOperationId);

            _referralStakesStatusUpdaterMock.Verify();
        }

        [Fact]
        public async Task HandleAsync_OperationIsInStatusReservationStarted_StatusUpdatedCalledToUpdateStatusToSucceeded()
        {
            _stakeBlockchainDataRepoMock.Setup(x => x.GetByOperationIdAsync(FakeOperationId))
                .ReturnsAsync(new StakesBlockchainEntity { StakeId = FakeReferralId });

            _referralStakesRepoMock.Setup(x => x.GetByReferralIdAsync(FakeReferralId, null))
                .ReturnsAsync(new ReferralStakeModel
                {
                    Status = StakeStatus.TokensReservationStarted
                });

            _referralStakesStatusUpdaterMock.Setup(x => x.TokensReservationSucceedAsync(FakeReferralId))
                .ReturnsAsync(ReferralStakesStatusUpdateErrorCode.None);

            var sut = CreateSutInstance();

            await sut.HandleAsync(FakeOperationId);

            _referralStakesStatusUpdaterMock.Verify(x => x.TokensReservationSucceedAsync(FakeReferralId), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_OperationIsInStatusBurnAndReleaseStarted_StatusUpdatedCalledToUpdateStatusToSucceeded()
        {
            _stakeBlockchainDataRepoMock.Setup(x => x.GetByOperationIdAsync(FakeOperationId))
                .ReturnsAsync(new StakesBlockchainEntity { StakeId = FakeReferralId });

            _referralStakesRepoMock.Setup(x => x.GetByReferralIdAsync(FakeReferralId, null))
                .ReturnsAsync(new ReferralStakeModel
                {
                    Status = StakeStatus.TokensBurnAndReleaseStarted
                });

            _referralStakesStatusUpdaterMock.Setup(x => x.TokensBurnAndReleaseSucceedAsync(FakeReferralId))
                .ReturnsAsync(ReferralStakesStatusUpdateErrorCode.None);

            var sut = CreateSutInstance();

            await sut.HandleAsync(FakeOperationId);

            _referralStakesStatusUpdaterMock.Verify(x => x.TokensBurnAndReleaseSucceedAsync(FakeReferralId), Times.Once);
        }

        private TransactionSucceededEventHandler CreateSutInstance()
        {
            return new TransactionSucceededEventHandler(
                _referralStakesRepoMock.Object,
                _stakeBlockchainDataRepoMock.Object,
                _referralStakesStatusUpdaterMock.Object,
                EmptyLogFactory.Instance);
        }
    }
}

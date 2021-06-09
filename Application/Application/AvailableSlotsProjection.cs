using System.Threading.Tasks;
using Application.Domain.ReadModel;
using Application.Domain.WriteModel.Events;
using Application.Infrastructure.Projections;

namespace Application.Application
{
    public class AvailableSlotsProjection : Projection
    {
        public AvailableSlotsProjection(IAvailableSlotsRepository repo)
        {
            When<Scheduled>(s =>
            {
                repo.Add(new AvailableSlot(s.SlotId, s.StartTime, s.Duration));
                return Task.CompletedTask;
            });

            When<Booked>(s =>
            {
                repo.MarkAsUnavailable(s.SlotId);
                return Task.CompletedTask;
            });

            When<Cancelled>(s =>
            {
                repo.MarkAsAvailable(s.SlotId);
                return Task.CompletedTask;
            });

        }
    }

}

using System.Threading.Tasks;
using Application.Domain.ReadModel;
using Application.Domain.WriteModel.Events;
using Application.Infrastructure.Projections;

namespace Application.Application
{
    public class PatientSlotsProjection : Projection
    {
        public PatientSlotsProjection(IPatientSlotsRepository repo)
        {
            When<Scheduled>(s =>
            {
                repo.Add(new ScheduledSlot(s.SlotId, s.StartTime, s.Duration));
                return Task.CompletedTask;
            });

            When<Booked>(s =>
            {
                repo.MarkAsBooked(s.SlotId, s.PatientId);
                return Task.CompletedTask;
            });

            When<Cancelled>(s =>
            {
                repo.MarkAsCancelled(s.SlotId);
                return Task.CompletedTask;
            });

            
        }
    }

}
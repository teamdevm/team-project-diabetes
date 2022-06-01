using Diabetes.Application.Interfaces;
using System;
using System.Threading;

namespace Diabetes.Application
{
    static class InsulinActionsDb
    {
        public static async void AddToDb(INoteInsulinDbContext dbContext, 
            Domain.NoteInsulin noteInsulin, CancellationToken cancellationToken)
        {
            await dbContext.InsulinNotes.AddAsync(noteInsulin, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public static async void EditInDb(INoteInsulinDbContext dbContext,
            Domain.NoteInsulin noteInsulin, Guid noteInsulinGuid, CancellationToken cancellationToken)
        {
            var entity = new Domain.NoteInsulin
            {
                UserId = noteInsulin.UserId,
                Id = noteInsulinGuid,
                InsulinValue = noteInsulin.InsulinValue,
                MeasuringDateTime = noteInsulin.MeasuringDateTime,
                InsulinType = noteInsulin.InsulinType,
                Comment = noteInsulin.Comment
            };

            dbContext.InsulinNotes.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public static async void EditInDb(INoteInsulinDbContext dbContext, Guid userId, Guid noteInsulinGuid, 
            double insulinValue, DateTime measuringDateTime, string insulinType, string comment,
            CancellationToken cancellationToken)
        {
            var entity = new Domain.NoteInsulin
            {
                UserId = userId,
                Id = noteInsulinGuid,
                InsulinValue = insulinValue,
                MeasuringDateTime = measuringDateTime,
                InsulinType = insulinType,
                Comment = comment
            };

            dbContext.InsulinNotes.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public static async void DeleteFromDb(INoteInsulinDbContext dbContext, 
            Guid noteInsulinGuid, CancellationToken cancellationToken)
        {
            Domain.NoteInsulin entity = GetEntity(dbContext, noteInsulinGuid);

            if (entity != null)
            {
                dbContext.InsulinNotes.Remove(entity);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public static Domain.NoteInsulin GetEntity(INoteInsulinDbContext dbContext, Guid noteInsulinGuid)
        {
            return dbContext.InsulinNotes.Find(noteInsulinGuid);
        }
    }
}

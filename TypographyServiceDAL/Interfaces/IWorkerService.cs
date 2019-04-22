using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System.Collections.Generic;


namespace TypographyServiceDAL.Interfaces
{
    public interface IWorkerService
    {
        List<WorkerViewModel> GetList();
        WorkerViewModel GetElement(int id);
        void AddElement(WorkerBindingModel model);
        void UpdElement(WorkerBindingModel model);
        void DelElement(int id);
        WorkerViewModel GetFreeWorker();
    }
}

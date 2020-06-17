using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAppReCaller.Services
{
    /// <summary>
    /// Taken from https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding
    /// Figure 4
    /// </summary>
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public Task<TResult> Task { get; private set; }
        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion)
                    ? Task.Result
                    : default(TResult);
            }
        }
        public TaskStatus Status { get { return Task.Status; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsNotCompleted { get { return !Task.IsCompleted; } }
        public bool IsSuccessfullyCompleted { get { return Task.Status == TaskStatus.RanToCompletion; } }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException
        {
            get
            {
                return (Exception == null)
                    ? null
                    : Exception.InnerException;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return (InnerException == null)
                    ? null
                    : InnerException.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyTaskCompletion(TResult syncItem)
        {
            var task = new Task<TResult>(() => { return syncItem; });
            // If we don't start the task, the thread will lock when the task is awaited upon
            task.Start();            
            SetUpTask(task);
        }

        public NotifyTaskCompletion(Task<TResult> task)
        {
            SetUpTask(task);
        }

        private void SetUpTask(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch { }

            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
            {
                return;
            }

            propertyChanged(this, new PropertyChangedEventArgs("Status"));
            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));

            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("Result"));
            }


        }
    }
}

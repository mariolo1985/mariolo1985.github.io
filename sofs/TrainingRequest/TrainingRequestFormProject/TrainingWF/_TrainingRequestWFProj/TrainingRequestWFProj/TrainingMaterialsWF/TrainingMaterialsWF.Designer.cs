using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace TrainingRequestWFProj.TrainingMaterialsWF
{
    public sealed partial class TrainingMaterialsWF
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken3 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken4 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            this.wfTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.completeTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
            this.LogStatus = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.WhileTaskIsActive = new System.Workflow.Activities.WhileActivity();
            this.onTaskCreated = new Microsoft.SharePoint.WorkflowActions.OnTaskCreated();
            this.LogContentType = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskByContentType = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.TaskSequence = new System.Workflow.Activities.SequenceActivity();
            this.LoopStages = new System.Workflow.Activities.WhileActivity();
            this.LogStart = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.workflowInitiated = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // wfTaskChanged
            // 
            activitybind1.Name = "TrainingMaterialsWF";
            activitybind1.Path = "WFTaskAfterProperties";
            activitybind2.Name = "TrainingMaterialsWF";
            activitybind2.Path = "WFTaskBeforeProperties";
            correlationtoken1.Name = "CreateTaskByCTToken";
            correlationtoken1.OwnerActivityName = "TaskSequence";
            this.wfTaskChanged.CorrelationToken = correlationtoken1;
            this.wfTaskChanged.Executor = null;
            this.wfTaskChanged.Name = "wfTaskChanged";
            activitybind3.Name = "TrainingMaterialsWF";
            activitybind3.Path = "TrainingTaskId";
            this.wfTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.OnTaskChanged);
            this.wfTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.wfTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.wfTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // completeTask
            // 
            correlationtoken2.Name = "CreateTaskByCTToken";
            correlationtoken2.OwnerActivityName = "TaskSequence";
            this.completeTask.CorrelationToken = correlationtoken2;
            this.completeTask.Name = "completeTask";
            activitybind4.Name = "TrainingMaterialsWF";
            activitybind4.Path = "TrainingTaskId";
            activitybind5.Name = "TrainingMaterialsWF";
            activitybind5.Path = "CompleteTaskOutcome";
            this.completeTask.MethodInvoking += new System.EventHandler(this.CompleteTask_MethodInvoking);
            this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // LogStatus
            // 
            this.LogStatus.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.LogStatus.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind6.Name = "TrainingMaterialsWF";
            activitybind6.Path = "LogHistory";
            activitybind7.Name = "TrainingMaterialsWF";
            activitybind7.Path = "LogHistoryOutcome";
            this.LogStatus.Name = "LogStatus";
            this.LogStatus.OtherData = "";
            activitybind8.Name = "TrainingMaterialsWF";
            activitybind8.Path = "LogHistoryUserGuid";
            this.LogStatus.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.LogStatus.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.LogStatus.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // WhileTaskIsActive
            // 
            this.WhileTaskIsActive.Activities.Add(this.wfTaskChanged);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TaskIsActive);
            this.WhileTaskIsActive.Condition = codecondition1;
            this.WhileTaskIsActive.Name = "WhileTaskIsActive";
            // 
            // onTaskCreated
            // 
            activitybind9.Name = "TrainingMaterialsWF";
            activitybind9.Path = "WFTaskAfterProperties";
            correlationtoken3.Name = "CreateTaskByCTToken";
            correlationtoken3.OwnerActivityName = "TaskSequence";
            this.onTaskCreated.CorrelationToken = correlationtoken3;
            this.onTaskCreated.Executor = null;
            this.onTaskCreated.Name = "onTaskCreated";
            activitybind10.Name = "TrainingMaterialsWF";
            activitybind10.Path = "TrainingTaskId";
            this.onTaskCreated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.OnTaskCreatedEvent);
            this.onTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskCreated.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.onTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskCreated.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            // 
            // LogContentType
            // 
            this.LogContentType.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.LogContentType.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind11.Name = "TrainingMaterialsWF";
            activitybind11.Path = "LogHistory";
            activitybind12.Name = "TrainingMaterialsWF";
            activitybind12.Path = "LogHistoryOutcome";
            this.LogContentType.Name = "LogContentType";
            this.LogContentType.OtherData = "";
            this.LogContentType.UserId = -1;
            this.LogContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.LogContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // createTaskByContentType
            // 
            activitybind13.Name = "TrainingMaterialsWF";
            activitybind13.Path = "TaskContentTypeId";
            this.createTaskByContentType.CorrelationToken = correlationtoken3;
            this.createTaskByContentType.ListItemId = -1;
            this.createTaskByContentType.Name = "createTaskByContentType";
            this.createTaskByContentType.SpecialPermissions = null;
            activitybind14.Name = "TrainingMaterialsWF";
            activitybind14.Path = "TrainingTaskId";
            activitybind15.Name = "TrainingMaterialsWF";
            activitybind15.Path = "TrainingTaskProperties";
            this.createTaskByContentType.MethodInvoking += new System.EventHandler(this.CreateTaskByContentType_MethodInvoking);
            this.createTaskByContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.ContentTypeIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.createTaskByContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.createTaskByContentType.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // TaskSequence
            // 
            this.TaskSequence.Activities.Add(this.createTaskByContentType);
            this.TaskSequence.Activities.Add(this.LogContentType);
            this.TaskSequence.Activities.Add(this.onTaskCreated);
            this.TaskSequence.Activities.Add(this.WhileTaskIsActive);
            this.TaskSequence.Activities.Add(this.LogStatus);
            this.TaskSequence.Activities.Add(this.completeTask);
            this.TaskSequence.Name = "TaskSequence";
            // 
            // LoopStages
            // 
            this.LoopStages.Activities.Add(this.TaskSequence);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.LoopUntil);
            this.LoopStages.Condition = codecondition2;
            this.LoopStages.Name = "LoopStages";
            // 
            // LogStart
            // 
            this.LogStart.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.LogStart.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind16.Name = "TrainingMaterialsWF";
            activitybind16.Path = "LogHistory";
            activitybind17.Name = "TrainingMaterialsWF";
            activitybind17.Path = "LogHistoryOutcome";
            this.LogStart.Name = "LogStart";
            this.LogStart.OtherData = "";
            this.LogStart.UserId = -1;
            this.LogStart.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.LogStart.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            activitybind19.Name = "TrainingMaterialsWF";
            activitybind19.Path = "workflowId";
            // 
            // workflowInitiated
            // 
            correlationtoken4.Name = "workflowToken";
            correlationtoken4.OwnerActivityName = "TrainingMaterialsWF";
            this.workflowInitiated.CorrelationToken = correlationtoken4;
            this.workflowInitiated.EventName = "OnWorkflowActivated";
            this.workflowInitiated.Name = "workflowInitiated";
            activitybind18.Name = "TrainingMaterialsWF";
            activitybind18.Path = "workflowProperties";
            this.workflowInitiated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.WorkflowInitiated_Invoked);
            this.workflowInitiated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.workflowInitiated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // TrainingMaterialsWF
            // 
            this.Activities.Add(this.workflowInitiated);
            this.Activities.Add(this.LogStart);
            this.Activities.Add(this.LoopStages);
            this.Name = "TrainingMaterialsWF";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.CompleteTask CompleteTask;
        private Microsoft.SharePoint.WorkflowActions.OnTaskCreated OnTaskCreated;
        private Microsoft.SharePoint.WorkflowActions.CompleteTask completeTask;
        private Microsoft.SharePoint.WorkflowActions.OnTaskCreated onTaskCreated;
        private WhileActivity WhileTaskIsActive;
        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity LogStatus;
        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged wfTaskChanged;
        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity LogContentType;
        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskByContentType;
        private SequenceActivity TaskSequence;
        private WhileActivity LoopStages;
        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity LogStart;
        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated workflowInitiated;







































    }
}

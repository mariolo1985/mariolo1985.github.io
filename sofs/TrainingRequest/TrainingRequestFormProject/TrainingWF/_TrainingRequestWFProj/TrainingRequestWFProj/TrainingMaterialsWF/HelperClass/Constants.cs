﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingRequestWFProj.TrainingMaterialsWF.HelperClass
{
    public partial class Constants
    {
        // **************************** Configuration ******************************
        public static string ConfigListUrl = "/lists/reviewconfigurationlist";
        public static string TaskOwnerListUrl = "/lists/taskownerlist";
        public static string DeptListUrl = "/lists/departments";
        public static string TaskOwnerRolesUrl = "/lists/taskownerroles";
        public static string RequestTypeListUrl = "/lists/requesttypes";


        public static string StageLifecycleColumnName = "RequestStageLifecycle";
        public static string TaskOwnerRoleColumnName = "TaskOwnerRole";
        public static string RequestTypeColumnName = "RequestType";        
        public static string RequestStageOrderColumnName = "RequestStageOrder";        
        public static string RoleCoumnName = "Role";
        public static string DeptColumnName = "Department";
        public static string RequestTypeListColumnName = "Title";

        public static string ReviewTaskContentType = "ReviewTask";
        public static string AssignTaskContentType = "AssignTask";

        public static string AssignTaskLifecycle = "Assigning Task Owner";
        public static string DeptAdminColumnName = "AdministratorGroup";


        // **************************** Metadata ******************************
        // Roles
        public static string TrainerRoleString = "[Trainer]";
        public static string TrainingManagerRoleString = "[Training Manager]";

        // Form Library
        public static string ItemLifecycleColumnName = "LifecycleStatus";
        public static string ItemRequestTypeColumnName = "RequestType";
        public static string ItemRequestTitleColumnName = "RequestTitle";
        public static string RequestStageColumnName = "RequestStage";
        
        public static string ItemDeptColumnName = "TrainingDepartment";
        public static string TaskOwnerStrColumnName = "TaskOwnerStr";
        public static string TaskOwnerPeopleColumnName = "RequestTaskOwner";
        public static string TrainerColumnName = "Trainer";// this will only be used during a planned training on TrainingMaterialsWF.cs

        // **************************** Config/Metadata ******************************
        public static string PlannedTrainingColumnName = "PlannedTraining";        
        public static string TaskOwnerColumnName = "TaskOwner";

        // **************************** Task ******************************
        public static string TaskListUrl = "/lists/trainingtasks";


    }// end propertybag class
}

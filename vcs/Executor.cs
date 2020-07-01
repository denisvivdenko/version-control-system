using System;
using System.Collections.Generic;
using vcs.Commands;


namespace vcs
{

    public class Executor
    {
        private Command _command;
        
        public Executor(CommandParser parser)
        {
            _command = parser.GetContent();
        }

        public void Run()
        {
            if (_command.CommandName == "init") new RepoInitializer(_command).InitializeRepo(); 
            if (_command.CommandName == "status") new StageStatus(_command.CallDirectory).GetCurrentStage().PrintInformation();
            if (_command.CommandName == "add") new StageDirector(_command).Execute();
        }
    }
}
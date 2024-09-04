using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueCommandExecuter
{
    static Dictionary<string, System.Type> commandToType = new Dictionary<string, System.Type>()
    {
        { "Kill", typeof(KillCommand) },
        { "Teleport", typeof(TeleportCommand) },
        { "Move", typeof(MoveCommand) },
        //{ "AddExperience", typeof(AddExperienceCommand) },
        //{ "Teleport", typeof(TeleportCommand) }
    };

    public static void ExecuteActions(string actions, GameObject target)
    {
        // Split actions by commas and remove extra spaces
        string[] actionStrings = actions.Split(new[] { "), " }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string actionString in actionStrings)
        {
            Debug.Log(actionString);

            // Remove any leading/trailing spaces and closing parenthesis
            string trimmedActionString = actionString.Trim().TrimEnd(')');

            // Find the position of the first parenthesis to separate action name from parameters
            int openParenIndex = trimmedActionString.IndexOf('(');
            if (openParenIndex == -1)
            {
                Debug.LogWarning("Invalid action format: missing opening parenthesis.");
                return;
            }

            string actionName = trimmedActionString.Substring(0, openParenIndex).Trim();
            string parametersString = trimmedActionString.Substring(openParenIndex + 1).Trim();

            // Parse parameters
            List<object> parameters = new List<object>();
            if (string.IsNullOrEmpty(parametersString) == false)
            {
                // Remove quotes from string parameters
                parametersString = parametersString.Replace("\"", "");

                // Split parameters by commas
                string[] paramsArray = parametersString.Split(',');

                foreach (string param in paramsArray)
                {
                    string trimmedParam = param.Trim();
                    if (float.TryParse(trimmedParam, out float intValue))
                    {
                        Debug.Log(intValue);
                        parameters.Add(intValue);
                    }
                    else
                    {
                        Debug.Log("triummed" + trimmedParam);
                        parameters.Add(trimmedParam);
                    }
                }
            }

            // Create and execute command
            ICommand command = CreateCommand(actionName, target, parameters.ToArray());
            //command.Execute();
        }
    }

    public static ICommand CreateCommand(string actionName, params object[] parameters)
    {
        if (commandToType.TryGetValue(actionName, out System.Type commandType))
            return (ICommand)System.Activator.CreateInstance(commandType, parameters);

        Debug.LogError($"No command found for action: {actionName}");
        return null;
    }



    //public static void ExecuteCommand(string commandType, GameObject target)
    //{
    //    ICommand command = commandType switch
    //    {
    //        "Kill();" => new KillCommand(target),
    //        // Add more cases for other commands
    //        _ => throw new System.ArgumentException($"Invalid command type: {commandType}")
    //    };

    //    command.Execute();
    //}
}


namespace MacketRouter.Physical;

interface IBoard
{
    IBoard Decode(string[] placements);
    string[] Encode();
    /// <summary> Shows how many element placements have been done. </summary>
    int ActionsCount { get; }
    IHub[] AvailableHubs { get; }
    /// <summary> Searches the hub where concrete elements is placed. </summary>
    IHub FindHubForElementByName();
}
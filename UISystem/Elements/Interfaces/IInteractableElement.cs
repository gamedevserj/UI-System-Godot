namespace UISystem.Core.Elements
{
    /// <summary>
    /// Defines contract for focusable UI element in Godot.
    /// </summary>
    public partial interface IInteractableElement
    {
        /// <summary>
        /// Switches element focus.
        /// </summary>
        /// <param name="focus">Whether element should have focus.</param>
        void SwitchFocus(bool focus);

        /// <summary>
        /// Checks whether this element is valid.
        /// </summary>
        /// <returns>True if element is valid, otherwise - false.</returns>
        bool IsValidElement();

        /// <summary>
        /// Switches elements focus mode and mouse filter.
        /// </summary>
        /// <param name="focusable">Whether element should be allowed to have focus/be interactable.</param>
        void SwitchFocusAvailability(bool focusable);
    }
}

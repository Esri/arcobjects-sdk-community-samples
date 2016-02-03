using System;
using System.Collections.Generic;
using System.Text;

namespace EditingUsingCustomForm
{
    class EditHelper
    {
        private static EditHelper instance;
        private MainForm m_mainform = null;
        private bool m_editorFormOpen;

        //private constructor - external classes cannot create a 'new' EditHelper instance
        private EditHelper()
        {
        }

        public static MainForm TheMainForm
        {
            get
            {
                if (instance != null)
                {
                    return instance.m_mainform;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }

                instance.m_mainform = value;

            }
        }

        public static bool IsEditorFormOpen
        {
            get
            {
                if (instance != null)
                {
                    return instance.m_editorFormOpen;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (instance == null)
                {
                    instance = new EditHelper();
                }

                instance.m_editorFormOpen = value;

            }
        }




    }
}

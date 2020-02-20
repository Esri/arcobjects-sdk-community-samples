/*

   Copyright 2019 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
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

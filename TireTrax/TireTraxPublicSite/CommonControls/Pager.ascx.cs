using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonControls_Pager : UserControl
{
    bool _showAllRecords = true;

    public bool ShowAllRecords
    {
        get
        {
            return _showAllRecords;
        }

        set
        {
            _showAllRecords = value;
        }

    }

    public int DrawPager(int currentPage, int totalItems, int pageSize, int maxPagesToShow)
    {
        this.rowPager.Cells.Clear();

        int totalPages = totalItems / pageSize;

        if (totalItems % pageSize != 0)
        {
            totalPages++;
        }

        maxPagesToShow = 5;
        int startPage = 0;
        int endPage = totalPages;

        if (totalPages > 0)
        {
            LinkButton buttonfirstPager = new LinkButton();

            buttonfirstPager.ID = string.Format("Button_{0}", 1);
            if (currentPage >= (maxPagesToShow - 2))
                buttonfirstPager.Text = "First";
            else
                buttonfirstPager.Text = "1";
            buttonfirstPager.EnableTheming = false;
            buttonfirstPager.Click += buttonPager_Click;


            TableCell newlastCell = new TableCell();
            if (currentPage == 1)
            {
                buttonfirstPager.Enabled = false;
                // newlastCell.CssClass = "this-page";
                buttonfirstPager.CssClass = "this-page";
            }
            newlastCell.Controls.Add(buttonfirstPager);

            this.rowPager.Cells.Add(newlastCell);

        }
        if (totalPages > maxPagesToShow)
        {
            int pagesPerSide = maxPagesToShow / 2;
            startPage = currentPage - pagesPerSide;
            if (startPage < 0)
            {
                startPage = 0;
            }

            endPage = startPage + maxPagesToShow;

            if (endPage > totalPages)
            {
                endPage = totalPages;
            }
        }

        for (int i = startPage; i < endPage - 1; i++)
        {
            if (i != 0)
            {
                LinkButton buttonPager = new LinkButton();
                buttonPager.ID = string.Format("Button_{0}", i + 1);
                buttonPager.Text = (i + 1).ToString();
                buttonPager.EnableTheming = false;
                buttonPager.Click += buttonPager_Click;
                if (i == currentPage - 1)
                {
                    buttonPager.Enabled = false;
                    // buttonPager.BackColor = Color.LightGray;
                }

                TableCell newCell = new TableCell();
                if (i == currentPage - 1)
                {
                    //newCell.CssClass = "this-page";
                    buttonPager.CssClass = "this-page";
                }
                newCell.Controls.Add(buttonPager);

                this.rowPager.Cells.Add(newCell);
            }
        }

        if (totalPages > 1)
        {
            LinkButton buttonlastPager = new LinkButton();
            buttonlastPager.ID = string.Format("Button_{0}", totalPages);
            /* if (currentPage < (totalPages - (maxPagesToShow + 2)))
                 buttonlastPager.Text = "Last";
             else
                 buttonlastPager.Text = totalPages.ToString();
             */
            if (currentPage > (totalPages - (maxPagesToShow - 2)))
                buttonlastPager.Text = totalPages.ToString();
            else
                buttonlastPager.Text = "Last";
            buttonlastPager.EnableTheming = false;
            buttonlastPager.Click += buttonPager_Click;


            TableCell newlastCell = new TableCell();
            if (currentPage == totalPages)
            {
                buttonlastPager.Enabled = false;
                // newlastCell.CssClass = "this-page";
                buttonlastPager.CssClass = "this-page";
            }
            newlastCell.Controls.Add(buttonlastPager);

            this.rowPager.Cells.Add(newlastCell);


        }
        Label lblShowIllRecords = new Label();
        lblShowIllRecords.Visible = ShowAllRecords;
        lblShowIllRecords.Text = "     Total Records : " + totalItems.ToString();
        TableCell newCell1 = new TableCell();
        newCell1.Controls.Add(lblShowIllRecords);

        this.rowPager.Cells.Add(newCell1);

        return totalPages;
    }


    void buttonPager_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        linkButton.Font.Bold = true;
        int clickedPageNumber = Convert.ToInt32(linkButton.ID.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1]);

        this.RaiseBubbleEvent(this, new CommandEventArgs("PageNumber", clickedPageNumber));
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
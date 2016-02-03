<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<!-- A sample custom template
	
     Revision History: Created 05/03/00 avienneau
-->

<!-- When the root node in the XML is encountered (the metadata element), the HTML template is set up -->
<xsl:template match="/">
  <HTML>
  
    <!-- The HEAD section provides information that doesn't affect the content of the page. -->
    <HEAD>
	
		 <!-- The STYLE block defines the formatting and text styles for this page.  The BODY style
	 	defines the defaults for the entire page.  The following style classes customize the BODY
		style, and can be applied to any HTML element on the page by setting its CLASS property -->
      <STYLE>
        BODY {margin-left:0.25in; margin-right:0.4in; background-color:#FFFFFF; 
              font-size:10pt; font-family:Arial,sans-serif; color:#800000}

        TABLE {font-size:10pt; borderStyle:solid; border-collapse:collapse}

        .title {font-size:22; font-weight:bold; text-decoration:underline; 
                text-align:center}

        .entity {font-weight:bold; font-size:16; color:#B22222}

        .attribute {font-weight:bold; font-size:14; color:#8B0000}

        .heading {color:#D2691E}

      </STYLE>
    </HEAD>

	<!-- The BODY section defines the content of the HTML page.  The style of the page is defined 
		above.  This page has a 1/4 inch margin on both sides, a background colour, and a default font look -->	
    <BODY>
	
	<!--  Add a title to the page.  It's look will be as defined by the styles in the head
		section above: centered, large bold font and underlined -->	
      <DIV CLASS="title">Data Dictionary</DIV>
      <BR/>
      
  	  <!--  DETAILED.  There is one detailed element in the metadata for each entity in the data source.
		  	This xsl:for-each selects these detailed elements -->
      <xsl:for-each select="metadata/eainfo/detailed">
	  
	  	<!-- ENTITY NAME.  Add a heading with the name of the entity described by the currently selected
			detailed element. -->	  
        <DIV CLASS="entity">
          Attributes of <xsl:value-of select="enttyp/enttypl"/>
        </DIV>
		
		<!-- ENTITY DESCRIPTION.  Add a type and description, including the authority source for
			the currently selected detailed element. -->		
        <xsl:for-each select="enttyp/enttypt[. != '']">
          <DIV STYLE="margin-left:0.25in"><I>Type of object: </I><xsl:value-of /></DIV>
          <BR/>
        </xsl:for-each>
        <xsl:for-each select="enttyp/enttypd[. != '']">
          <DIV STYLE="margin-left:0.25in">
            <xsl:value-of />
            <xsl:for-each select="../enttypds"> (<I>Source: </I><xsl:value-of />)</xsl:for-each>
          </DIV>
        </xsl:for-each>

        <BR/><BR/>

		<!-- This for-each statement selects only those attributes that that have the label property defined. -->
		 <xsl:for-each select="attr[attrlabl != '']">

		<!-- Write out the name of the attribute -->
          <DIV STYLE="margin-left:0.25in">
            <xsl:for-each select="attrlabl">
              <DIV CLASS="attribute"><xsl:value-of /></DIV>
            </xsl:for-each>

			<!-- Write out the attribute's description if it exists.  If it doesn't, write "no description".
				Also, write out the source if it exists -->
            <xsl:if test="context()[attrdef != '']">
              <DIV STYLE="margin-left:0.25in">
                <xsl:choose>
                  <xsl:when test="attrdef[. != '']"><xsl:value-of select="attrdef"/></xsl:when>
                  <xsl:otherwise>No description</xsl:otherwise>
                </xsl:choose>
                <xsl:for-each select="attrdefs"> (<I>Source: </I><xsl:value-of />)</xsl:for-each>
              </DIV>
              <BR/>
            </xsl:if>

			<!-- Test to see if any of the following properties are defined for the currently selected
				attribute: Type, Width, Output Width, Decimals, Precision, Scale, Alias.  If none of 
				those are defined, the following <xsl:if> code section will be ignored. -->					
            <xsl:if test="context()[(attrtype != '') or (attwidth != '') or (atoutwid != '') or 
                (atnumdec != '') or (atprecis != '') or (attscale != '') or (attalias != '')]">

			<!-- Create a table for writing out the properties of the currently selected attribute.
				Each row of the table is dedicated to a separate property.  There are 2 columns:  
				The first one holds the property name and the second holds the definition or value -->
              <DIV CLASS="heading" STYLE="margin-left:0.25in"><B>Column Definition</B></DIV>
              <TABLE STYLE="margin-left:0.4in" CELLPADDING="3">

				<!-- Write out the attribute's "Type" property if it is defined. -->	
                <xsl:for-each select="attrtype[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Data type</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                </xsl:for-each>

				<!-- Write out the attribute's "Width" property if it is defined. -->				
                <xsl:for-each select="attwidth[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Width</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                </xsl:for-each>

				<!-- Write out the attribute's "Output Width" property if it is defined. -->				
                <xsl:for-each select="atoutwid[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Output width</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR> 
                </xsl:for-each>

				<!-- Write out the attribute's "Decimal" property if it is defined. -->				
                <xsl:for-each select="atnumdec[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Decimals</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                </xsl:for-each>

				<!-- Write out the attribute's "Precision" property if it is defined. -->								
                <xsl:for-each select="atprecis[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Precision</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                </xsl:for-each>

				<!-- Write out the attribute's "Scale" property if it is defined. -->								
                <xsl:for-each select="attscale[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Scale</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR> 
                </xsl:for-each>
				
				<!-- Write out the attribute's "Alias" property if it is defined. -->								
                <xsl:for-each select="attalias[. != '']">
                  <TR><TD STYLE="text-align:right"><I>Alias</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                </xsl:for-each>
              </TABLE> <!-- end of table -->
              <BR/>
            </xsl:if>

			<!-- Test to see if any of the following properties are defined for the currently selected
				attribute: Domain Values or Measured Frequency.  If none of those are defined,
				the following <xsl:if> code section will be ignored. -->					
            <xsl:if test="context()[(attrmfrq != '') or ($any$ attrdomv/* != '')]">
              <DIV STYLE="margin-left:0.25in">
                <xsl:for-each select="attrdomv/udom[. != '']">
                  <DIV CLASS="heading" STYLE="margin-bottom:0.05in"><B>Values - unrepresentable domain: </B></DIV>
                  <DIV STYLE="margin-left:0.2in"><xsl:value-of/></DIV><BR/>
                </xsl:for-each>

				<!-- Test if any of the attribute's domain value properties are defined.  If so, create a new 
					table to write the properties out to. In the first row will be the table headings, and 
					underneath each heading will be the corresponding data. -->
                <xsl:if test="attrdomv/edom[(edomv != '') or (edomvd != '')]">
                  <TABLE CELLPADDING="3">
				  
				  	<!-- Write out the table headings -->
                    <TR CLASS="heading"><TH STYLE="text-align:left">Value</TH><TH STYLE="text-align:left">Definition</TH></TR>
                    <xsl:for-each select="attrdomv/edom[(edomv != '') or (edomvd != '')]">

                      <TR>
						<!-- Write out the Value name -->
                        <TD STYLE="text-align:center" VALIGN="top"><xsl:value-of select="edomv"/></TD>

						<!-- Write out the Values's description -->
                        <TD STYLE="text-align:left">
                          <xsl:choose>
                            <xsl:when test="edomvd[. != '']">
                              <xsl:for-each select="edomvd[. != '']"><xsl:value-of/></xsl:for-each>
                            </xsl:when>
                            <xsl:otherwise xml:space="preserve">
                              <SPAN STYLE="color:#DEB887"> [not provided] </SPAN>
                            </xsl:otherwise>
                          </xsl:choose>
                        </TD>
                      </TR>
                    </xsl:for-each>
                  </TABLE> <!-- end table -->
                  <BR/>
                </xsl:if>
 
 				<!-- Test if any of the attribute's range domain values  are defined.  If so, create a new 
					table to write the properties out to.  -->
                <xsl:if test="attrdomv[(($any$ rdom/* != '') or (attrmfrq != ''))]">
                  <DIV CLASS="heading"><B>Range of values</B></DIV>

				<!-- Build the table.  Each row of the table is dedicated to a separate property.
					There are 2 columns:  The first one holds the property name and the second
					holds the definition or value -->
                  <TABLE STYLE="margin-left:0.15in" CELLPADDING="3">
                    <xsl:for-each select="attrdomv/rdom/rdommin[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Minimum</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                    </xsl:for-each>
                    <xsl:for-each select="attrdomv/rdom/rdommax[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Maximum</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                    </xsl:for-each>
                    <xsl:for-each select="attrdomv/rdom/attrunit[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Units</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR> 
                    </xsl:for-each>
                    <xsl:for-each select="attrdomv/rdom/attrmres[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Resolution</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                    </xsl:for-each>
                  </TABLE> <!-- end of table -->
				  
				  <!-- Write out the attribute's "frequency of measurement" property -->
                  <xsl:for-each select="attrmfrq[. != '']"><DIV STYLE="margin-top:0.15in"><I>Frequency of measurement:</I> <xsl:value-of/></DIV></xsl:for-each>
                  <BR/>
                </xsl:if>

				<!-- Test if the attribute's formal codset value property is defined.  If so, create a new 
					table to write the properties out to.  -->
                <xsl:if test="attrdomv/codesetd[$any$ * != '']">
                  <DIV CLASS="heading"><B>Values - formal codeset</B></DIV>

					<!-- Build the table.  Each row of the table is dedicated to a separate property. 
						There are 2 columns:  The first one holds the property name and the second holds
						the definition or value. -->
                  <TABLE STYLE="margin-left:0.15in" CELLPADDING="3">
                    <xsl:for-each select="attrdomv/codesetd/codesetn[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Codeset name</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                    </xsl:for-each>
                    <xsl:for-each select="attrdomv/codesetd/codesets[. != '']">
                      <TR><TD STYLE="text-align:right"><I>Codeset source</I></TD><TD STYLE="text-align:left"><xsl:value-of/></TD></TR>
                    </xsl:for-each>
                  </TABLE> <!-- end of table -->
                  <BR/>
                </xsl:if>
              </DIV>
            </xsl:if>
          </DIV>
		  
		<!-- SPACE.  If the current attribute is not the last in the set, add an empty line before the
			next attribute is added to the page. -->		  
          <xsl:if test="context()[not(end())]">
            <BR/>
          </xsl:if>
        </xsl:for-each>
		
		<!-- DIVIDER.  If the current attribute is not the last in the set, add an dividing line before the
			next attribute is added to the page. -->
        <xsl:if test="context()[not(end())]">
          <DIV STYLE="text-align:center">__________________</DIV><BR/>
        </xsl:if>
      </xsl:for-each>

      <BR/><BR/>
    </BODY>

  </HTML>
</xsl:template>
	
</xsl:stylesheet>
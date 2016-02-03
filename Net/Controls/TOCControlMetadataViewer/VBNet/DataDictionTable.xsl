<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<!-- A sample custom stylesheet template
	
     Revision History: Created 05/03/00 avienneau
	 				Modified 10/13/00 mlyszkiewicz
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
          <I>Type of object: </I><xsl:value-of />
          <P/>
        </xsl:for-each>
        <xsl:for-each select="enttyp/enttypd[. != '']">
            <xsl:value-of />
            <xsl:for-each select="../enttypds"> (<I>Source: </I><xsl:value-of />)</xsl:for-each>
        </xsl:for-each>
       <P/>

	   <!-- the rest of the information for the currently selected attribute will be presented in table format -->
		<table width="100%" cellspacing="2" cellpadding="0" border="1">
		
		<!-- write out the table headings as the first row of the table.  A table heading will exist if at
			least one attribute in the set of attributes for the selected entity has that property defined -->
		<tr>
	        <xsl:if test="context()[attr/attrlabl[. != '']]">
				<th>Attribute Name</th>
				<th>Description</th>

		        <xsl:if test="context()[attr/attrtype[. != '']]">
					<th>Data Type</th>
				</xsl:if>
		        <xsl:if test="context()[attr/attwidth[. != '']]">
					<th>Width</th>
				</xsl:if>
				<xsl:if test="context()[attr/atoutwid[. != '']]">
					<th>Output Width</th>
				</xsl:if>
				<xsl:if test="context()[attr/atnumdec[. != '']]">
					<th>Decimals</th>
				</xsl:if>
				<xsl:if test="context()[attr/atprecis[. != '']]">
					<th>Precision</th>
				</xsl:if>
				<xsl:if test="context()[attr/attscale[. != '']]">
					<th>Scale</th>
				</xsl:if>
				<xsl:if test="context()[attr/attalias[. != '']]">
					<th>Alias</th>
				</xsl:if>
				<xsl:if test="context()[attr/attrdomv/udom[. != '']]">
					<th>Values - unrepresentable domain</th>
				</xsl:if>			
				<xsl:if test="context()[attr/attrdomv/edom/* [. != '']]">
					<th>Value: Definition</th>
				</xsl:if>		
				<xsl:if test="context()[(attr/attrdomv/rdom/* [. != '']) or (attr/attrmfrq != '')]">
					<th>Range of Values</th>
				</xsl:if>			
				<xsl:if test="context()[attr/attrdomv/codesetd/* [. != '']]">
					<th>Values: Formal Codeset</th>
				</xsl:if>							
			</xsl:if>
		</tr>
			
		<!-- This for-each statement selects only those attributes that have the label property defined.  
			As the code loops through each attribute that is selected by the for-each statement, each
			attribute's information will be written out on a separate row. -->
        <xsl:for-each select="attr[attrlabl != '']">
		<tr>
			<!-- Write out the name of the attribute -->
			<xsl:for-each select="attrlabl">
				<th><DIV CLASS="attribute"><xsl:value-of /></DIV></th>
            </xsl:for-each>

			<!-- Write out the attribute's description if it exists.  If it doesn't, write "no description".
				Also, write out the source if it exists -->

			<td>
                <xsl:choose>
                  <xsl:when test="attrdef[. != '']"><xsl:value-of select="attrdef"/></xsl:when>
                  <xsl:otherwise>No description</xsl:otherwise>
                </xsl:choose>
                <xsl:for-each select="attrdefs">
					<xsl:if test="context()[. != '']">
						(<I>Source: </I><xsl:value-of />)
					</xsl:if>
				</xsl:for-each>
              <BR/>
			</td>

			<!-- Test to see if any of the following properties are defined for the currently selected
				attribute: Type, Width, Output Width, Decimals, Precision, Scale, Alias.  If none of 
				those are defined, the following <xsl:if> code section will be ignored. -->					
            <xsl:if test="context()[(attrtype != '') or (attwidth != '') or (atoutwid != '') or 
                (atnumdec != '') or (atprecis != '') or (attscale != '') or (attalias != '')]">
				
				<!-- Write the the different properties of an attribute if they are defined.  The code
					structure is the same for the various properties.  If a property is defined for the
					currently selected attribute, it will be written out.  If the corresponding heading
					for the property was written out at the begining of the table construction, but the 
					currently selected attribute does not have that particular property defnied, then 
					write out three dashes in the table cell.  This will prevent from any data that follows this
					particular "empty" property from being placed in the wrong column, just because one
					property was left out for one specific attribute.  The code for the first property
					is explained in detail.  All other properties follow the same code structure. -->
				
				<xsl:choose>
				<!-- Write out the attribute's "Type" property if it is defined. -->
				<xsl:when test="context()[(attrtype !='')]">
					<xsl:for-each select="attrtype">
						<td><xsl:value-of /></td>
					</xsl:for-each>
				</xsl:when>
				<xsl:otherwise>
				<!-- If the property was not defined, yet the heading "Type" exists because some other
					attribute in the currently selected entity has this property defined, then write out
					three dashes in the table cell, to avoid succeeding data to shift to incorrect columns -->
					<xsl:if test="..[($any$ attr/attrtype != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>				
				</xsl:choose>

				<!-- Write out the attribute's width property-->
				<xsl:choose>
				<xsl:when test="context()[(attwidth !='')]">
					<xsl:for-each select="attwidth">
						<td><xsl:value-of /></td>
					</xsl:for-each>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/attwidth != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>				
				</xsl:choose>
				
				<!-- Write out the attribute's output width property-->
				<xsl:choose>
				<xsl:when test="context()[(atoutwid != '')]">
				  	<xsl:for-each select="atoutwid">
						<td><xsl:value-of /></td>
					</xsl:for-each>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/atoutwid != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>
				</xsl:choose>

				<!-- Write out the attribute's decimal property-->
				<xsl:choose>
	            <xsl:when test="context()[(atnumdec != '')]">
					<xsl:for-each select="atnumdec">
						<td><xsl:value-of /></td>
					</xsl:for-each>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/atnumdec != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>
				</xsl:choose>

				<!-- Write out the attribute's precision property-->
				<xsl:choose>
				<xsl:when test="context()[(atprecis !='')]">
					<xsl:for-each select="atprecis">
						<td><xsl:value-of /></td>
					</xsl:for-each>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/atprecis != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>
				</xsl:choose>
			
				<!-- Write out the attribute's scale property-->
				<xsl:choose>
	            <xsl:when test="context()[(attscale != '')]">
					<xsl:for-each select="attscale">
						<td><xsl:value-of /></td>
					</xsl:for-each>
 				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/attscale != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>
				</xsl:choose>

				<!-- Write out the attribute's alias property-->
				<xsl:choose>
	            <xsl:when test="context()[(attalias != '')]">
					<xsl:for-each select="attalias">
						<td><xsl:value-of /></td>
					</xsl:for-each>
 				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="..[($any$ attr/attalias != '')]">
						<td>---</td>
					</xsl:if>
				</xsl:otherwise>
				</xsl:choose>
            </xsl:if>			
			
			<!-- Write out the attribute's "unrepresentable domain values" property-->
			<xsl:choose>
			<xsl:when test="context()[(attrdomv/udom[. != ''])]">
				<xsl:for-each select="attrdomv/udom">
					<td><xsl:value-of/></td>
				</xsl:for-each>
			</xsl:when>
			<xsl:otherwise>
					<xsl:if test="..[($any$ attr/attrdomv/udom != '')]">
						<td>---</td>
					</xsl:if>
			</xsl:otherwise>
			</xsl:choose>

			<!-- Write out the attribute's "values and definitions" property-->
			<xsl:choose>
			<xsl:when test="context()[(attrdomv/edom[(edomv != '') or (edomvd != '')])]">
			<td>
			<xsl:for-each select="attrdomv/edom[(edomv != '') or (edomvd != '')]">
				<xsl:if test="edomv[. != '']">
					<FONT COLOR="#0000AA"><xsl:value-of select="edomv"/>:</FONT> 
				</xsl:if>
				<xsl:choose>
                   	<xsl:when test="edomvd[. != '']">
						<xsl:for-each select="edomvd[. != '']">
							<xsl:value-of/>
						</xsl:for-each>
					</xsl:when>
                       <xsl:otherwise>
                       	not provided
                       </xsl:otherwise>
				</xsl:choose><BR/>				
			</xsl:for-each>
			</td>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="..[($any$ attr/attrdomv/edom/* != '')]">
					<td>---</td>
				</xsl:if>
			</xsl:otherwise>				
			</xsl:choose>

			<!-- Write out the attribute's "range of values" property-->
			<xsl:choose>
			<xsl:when test="context()[attrmfrq[. != ''] or attrdomv[($any$ rdom/* != '')]]">
			<td>
			<xsl:for-each select="attrdomv/rdom/rdommin[. != '']">
				<FONT COLOR="#0000AA">Min:</FONT> <xsl:value-of/>
				<BR/>
			</xsl:for-each>

			<xsl:for-each select="attrdomv/rdom/rdommax[. != '']">
				<FONT COLOR="#0000AA">Max:</FONT> <xsl:value-of/>
			<BR/>
			</xsl:for-each>

			<xsl:for-each select="attrdomv/rdom/attrunit[. != '']">
				<FONT COLOR="#0000AA">Units:</FONT> <xsl:value-of/>
			<BR/>			
			</xsl:for-each>

			<xsl:for-each select="attrdomv/rdom/attrmres[. != '']">
				<FONT COLOR="#0000AA">Resolution:</FONT> <xsl:value-of/>
			<BR/>	
			</xsl:for-each>

			<xsl:for-each select="attrmfrq[. != '']">
				<FONT COLOR="#0000AA">Frequency of Measurement:</FONT> <xsl:value-of/>
			</xsl:for-each>
			</td>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="..[($any$ attr/attrdomv/rdom/* != '') or ($any$ attr/attrmfreq != '')]">
					<td>---</td>
				</xsl:if>
			</xsl:otherwise>				
			</xsl:choose>
			
			<!-- Write out the attribute's "codeset" property-->
			<xsl:choose>
			<xsl:when test="context()[(attrdomv/codesetd[$any$ * != ''])]">
			<td>
            <xsl:for-each select="attrdomv/codesetd/codesetn[. != '']">
				Codeset Name: <xsl:value-of/> 
				<BR/>
			</xsl:for-each>

            <xsl:for-each select="attrdomv/codesetd/codesets[. != '']">
				Codeset Source: <xsl:value-of/>
			</xsl:for-each>
			</td>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="..[($any$ attr/attrdomv/codesetd/* != '')]">
					<td>---</td>
				</xsl:if>
			</xsl:otherwise>				
			</xsl:choose>
		 </tr>
        </xsl:for-each>

		</table>
		
		<!-- SPACE.  If the current attribute is not the last in the set, add an empty line before the
			next attribute is added to the page. -->
         <xsl:if test="context()[not(end())]">
            <BR/>
          </xsl:if>

		<!-- DIVIDER.  If the current attribute is not the last in the set, add an dividing line before the
					next attribute is added to the page. -->
        <xsl:if test="context()[not(end())]">
          <DIV STYLE="text-align:center">__________________</DIV><BR/>
		</xsl:if>
	</xsl:for-each>
    </BODY>

  </HTML>
</xsl:template>
	
</xsl:stylesheet>
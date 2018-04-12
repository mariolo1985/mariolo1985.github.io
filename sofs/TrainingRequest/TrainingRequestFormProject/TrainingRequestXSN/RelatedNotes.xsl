<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2013-11-15T19:36:11" xmlns:xd="http://schemas.microsoft.com/office/infopath/2003" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:xdExtension="http://schemas.microsoft.com/office/infopath/2003/xslt/extension" xmlns:xdXDocument="http://schemas.microsoft.com/office/infopath/2003/xslt/xDocument" xmlns:xdSolution="http://schemas.microsoft.com/office/infopath/2003/xslt/solution" xmlns:xdFormatting="http://schemas.microsoft.com/office/infopath/2003/xslt/formatting" xmlns:xdImage="http://schemas.microsoft.com/office/infopath/2003/xslt/xImage" xmlns:xdUtil="http://schemas.microsoft.com/office/infopath/2003/xslt/Util" xmlns:xdMath="http://schemas.microsoft.com/office/infopath/2003/xslt/Math" xmlns:xdDate="http://schemas.microsoft.com/office/infopath/2003/xslt/Date" xmlns:sig="http://www.w3.org/2000/09/xmldsig#" xmlns:xdSignatureProperties="http://schemas.microsoft.com/office/infopath/2003/SignatureProperties" xmlns:ipApp="http://schemas.microsoft.com/office/infopath/2006/XPathExtension/ipApp" xmlns:xdEnvironment="http://schemas.microsoft.com/office/infopath/2006/xslt/environment" xmlns:xdUser="http://schemas.microsoft.com/office/infopath/2006/xslt/User" xmlns:xdServerInfo="http://schemas.microsoft.com/office/infopath/2009/xslt/ServerInfo">
	<xsl:output method="html" indent="no"/>
	<xsl:template match="my:myFields">
		<html>
			<head>
				<meta content="text/html" http-equiv="Content-Type"></meta>
				<style controlStyle="controlStyle">@media screen 			{ 			BODY{margin-left:21px;background-position:21px 0px;} 			} 		BODY{color:windowtext;background-color:window;layout-grid:none;} 		.xdListItem {display:inline-block;width:100%;vertical-align:text-top;} 		.xdListBox,.xdComboBox{margin:1px;} 		.xdInlinePicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) } 		.xdLinkedPicture{margin:1px; BEHAVIOR: url(#default#urn::xdPicture) url(#default#urn::controls/Binder) } 		.xdHyperlinkBox{word-wrap:break-word; text-overflow:ellipsis;overflow-x:hidden; OVERFLOW-Y: hidden; WHITE-SPACE:nowrap; display:inline-block;margin:1px;padding:5px;border: 1pt solid #dcdcdc;color:windowtext;BEHAVIOR: url(#default#urn::controls/Binder) url(#default#DataBindingUI)} 		.xdSection{border:1pt solid transparent ;margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;} 		.xdRepeatingSection{border:1pt solid transparent;margin:0px 0px 0px 0px;padding:0px 0px 0px 0px;} 		.xdMultiSelectList{margin:1px;display:inline-block; border:1pt solid #dcdcdc; padding:1px 1px 1px 5px; text-indent:0; color:windowtext; background-color:window; overflow:auto; behavior: url(#default#DataBindingUI) url(#default#urn::controls/Binder) url(#default#MultiSelectHelper) url(#default#ScrollableRegion);} 		.xdMultiSelectListItem{display:block;white-space:nowrap}		.xdMultiSelectFillIn{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:left;}		.xdBehavior_Formatting {BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting);} 	 .xdBehavior_FormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting);} 	.xdExpressionBox{margin: 1px;padding:1px;word-wrap: break-word;text-overflow: ellipsis;overflow-x:hidden;}.xdBehavior_GhostedText,.xdBehavior_GhostedTextNoBUI{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#TextField) url(#default#GhostedText);}	.xdBehavior_GTFormatting{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_GTFormattingNoBUI{BEHAVIOR: url(#default#CalPopup) url(#default#urn::controls/Binder) url(#default#Formatting) url(#default#GhostedText);}	.xdBehavior_Boolean{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#BooleanHelper);}	.xdBehavior_Select{BEHAVIOR: url(#default#urn::controls/Binder) url(#default#SelectHelper);}	.xdBehavior_ComboBox{BEHAVIOR: url(#default#ComboBox)} 	.xdBehavior_ComboBoxTextField{BEHAVIOR: url(#default#ComboBoxTextField);} 	.xdRepeatingTable{BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; BORDER-COLLAPSE: collapse; WORD-WRAP: break-word;}.xdScrollableRegion{BEHAVIOR: url(#default#ScrollableRegion);} 		.xdLayoutRegion{display:inline-block;} 		.xdMaster{BEHAVIOR: url(#default#MasterHelper);} 		.xdActiveX{margin:1px; BEHAVIOR: url(#default#ActiveX);} 		.xdFileAttachment{display:inline-block;margin:1px;BEHAVIOR:url(#default#urn::xdFileAttachment);} 		.xdSharePointFileAttachment{display:inline-block;margin:2px;BEHAVIOR:url(#default#xdSharePointFileAttachment);} 		.xdAttachItem{display:inline-block;width:100%%;height:25px;margin:1px;BEHAVIOR:url(#default#xdSharePointFileAttachItem);} 		.xdSignatureLine{display:inline-block;margin:1px;background-color:transparent;border:1pt solid transparent;BEHAVIOR:url(#default#SignatureLine);} 		.xdHyperlinkBoxClickable{behavior: url(#default#HyperlinkBox)} 		.xdHyperlinkBoxButtonClickable{border-width:1px;border-style:outset;behavior: url(#default#HyperlinkBoxButton)} 		.xdPictureButton{background-color: transparent; padding: 0px; behavior: url(#default#PictureButton);} 		.xdPageBreak{display: none;}BODY{margin-right:21px;} 		.xdTextBoxRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:right;word-wrap:normal;} 		.xdRichTextBoxRTL{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:right;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTTextRTL{height:100%;width:100%;margin-left:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButtonRTL{margin-right:-21px;height:17px;width:20px;behavior: url(#default#DTPicker);} 		.xdMultiSelectFillinRTL{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;overflow:hidden;text-align:right;}.xdTextBox{display:inline-block;white-space:nowrap;text-overflow:ellipsis;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-align:left;word-wrap:normal;} 		.xdRichTextBox{display:inline-block;;padding:1px;margin:1px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow-x:hidden;word-wrap:break-word;text-overflow:ellipsis;text-align:left;font-weight:normal;font-style:normal;text-decoration:none;vertical-align:baseline;} 		.xdDTPicker{;display:inline;margin:1px;margin-bottom: 2px;border: 1pt solid #dcdcdc;color:windowtext;background-color:window;overflow:hidden;text-indent:0; layout-grid: none} 		.xdDTText{height:100%;width:100%;margin-right:22px;overflow:hidden;padding:0px;white-space:nowrap;} 		.xdDTButton{margin-left:-21px;height:17px;width:20px;behavior: url(#default#DTPicker);} 		.xdRepeatingTable TD {VERTICAL-ALIGN: top;}</style>
				<style tableEditor="TableStyleRulesID">TABLE.xdLayout TD {
	BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none
}
TABLE.msoUcTable TD {
	BORDER-TOP: 1pt solid; BORDER-RIGHT: 1pt solid; BORDER-BOTTOM: 1pt solid; BORDER-LEFT: 1pt solid
}
TABLE {
	BEHAVIOR: url (#default#urn::tables/NDTable)
}
</style>
				<style languageStyle="languageStyle">BODY {
	FONT-SIZE: 10pt; FONT-FAMILY: Calibri
}
SELECT {
	FONT-SIZE: 10pt; FONT-FAMILY: Calibri
}
TABLE {
	FONT-SIZE: 10pt; FONT-FAMILY: Calibri; TEXT-TRANSFORM: none; FONT-WEIGHT: normal; COLOR: black; FONT-STYLE: normal
}
.optionalPlaceholder {
	FONT-SIZE: 9pt; FONT-FAMILY: Calibri; FONT-WEIGHT: normal; COLOR: #333333; FONT-STYLE: normal; PADDING-LEFT: 20px; TEXT-DECORATION: none; BEHAVIOR: url(#default#xOptional)
}
.langFont {
	FONT-SIZE: 10pt; FONT-FAMILY: Calibri; WIDTH: 150px
}
.defaultInDocUI {
	FONT-SIZE: 9pt; FONT-FAMILY: Calibri
}
.optionalPlaceholder {
	PADDING-RIGHT: 20px
}
</style>
				<style themeStyle="urn:office.microsoft.com:themeSummer">TABLE {
	BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none
}
TD {
	BORDER-TOP-COLOR: #d8d8d8; BORDER-BOTTOM-COLOR: #d8d8d8; BORDER-RIGHT-COLOR: #d8d8d8; BORDER-LEFT-COLOR: #d8d8d8
}
TH {
	BORDER-TOP-COLOR: #000000; COLOR: black; BORDER-BOTTOM-COLOR: #000000; BORDER-RIGHT-COLOR: #000000; BACKGROUND-COLOR: #f2f2f2; BORDER-LEFT-COLOR: #000000
}
.xdTableHeader {
	COLOR: black; BACKGROUND-COLOR: #f2f2f2
}
.light1 {
	BACKGROUND-COLOR: #ffffff
}
.dark1 {
	BACKGROUND-COLOR: #000000
}
.light2 {
	BACKGROUND-COLOR: #f7f8f4
}
.dark2 {
	BACKGROUND-COLOR: #2b4b4d
}
.accent1 {
	BACKGROUND-COLOR: #6c9a7f
}
.accent2 {
	BACKGROUND-COLOR: #bb523d
}
.accent3 {
	BACKGROUND-COLOR: #c89d11
}
.accent4 {
	BACKGROUND-COLOR: #fccf10
}
.accent5 {
	BACKGROUND-COLOR: #568ea1
}
.accent6 {
	BACKGROUND-COLOR: #decf28
}
</style>
				<style tableStyle="Professional">TR.xdTitleRow {
	MIN-HEIGHT: 83px
}
TD.xdTitleCell {
	BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 14px; TEXT-ALIGN: center; PADDING-TOP: 32px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: bottom
}
TR.xdTitleRowWithHeading {
	MIN-HEIGHT: 69px
}
TD.xdTitleCellWithHeading {
	BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; TEXT-ALIGN: center; PADDING-TOP: 32px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: bottom
}
TR.xdTitleRowWithSubHeading {
	MIN-HEIGHT: 75px
}
TD.xdTitleCellWithSubHeading {
	BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 6px; TEXT-ALIGN: center; PADDING-TOP: 32px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: bottom
}
TR.xdTitleRowWithOffsetBody {
	MIN-HEIGHT: 72px
}
TD.xdTitleCellWithOffsetBody {
	BORDER-TOP: #bfbfbf 1pt solid; BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 2px; TEXT-ALIGN: left; PADDING-TOP: 32px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: bottom
}
TR.xdTitleHeadingRow {
	MIN-HEIGHT: 37px
}
TD.xdTitleHeadingCell {
	BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 14px; TEXT-ALIGN: center; PADDING-TOP: 0px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: top
}
TR.xdTitleSubheadingRow {
	MIN-HEIGHT: 70px
}
TD.xdTitleSubheadingCell {
	BORDER-RIGHT: #bfbfbf 1pt solid; PADDING-BOTTOM: 16px; PADDING-TOP: 8px; PADDING-LEFT: 22px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #ffffff; valign: top
}
TD.xdVerticalFill {
	BORDER-TOP: #bfbfbf 1pt solid; BORDER-BOTTOM: #bfbfbf 1pt solid; BORDER-LEFT: #bfbfbf 1pt solid; BACKGROUND-COLOR: #354d3f
}
TD.xdTableContentCellWithVerticalOffset {
	BORDER-RIGHT: #bfbfbf 1pt solid; BORDER-BOTTOM: #bfbfbf 1pt solid; PADDING-BOTTOM: 2px; TEXT-ALIGN: left; PADDING-TOP: 32px; PADDING-LEFT: 95px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #ffffff; valign: bottom
}
TR.xdTableContentRow {
	MIN-HEIGHT: 140px
}
TD.xdTableContentCell {
	BORDER-RIGHT: #bfbfbf 1pt solid; BORDER-BOTTOM: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 0px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #ffffff; valign: top
}
TD.xdTableContentCellWithVerticalFill {
	BORDER-RIGHT: #bfbfbf 1pt solid; BORDER-BOTTOM: #bfbfbf 1pt solid; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 1px; BORDER-LEFT: #bfbfbf 1pt solid; PADDING-RIGHT: 1px; BACKGROUND-COLOR: #ffffff; valign: top
}
TD.xdTableStyleOneCol {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px
}
TR.xdContentRowOneCol {
	MIN-HEIGHT: 45px; valign: center
}
TR.xdHeadingRow {
	MIN-HEIGHT: 27px
}
TD.xdHeadingCell {
	BORDER-TOP: #a6c2b2 1pt solid; BORDER-BOTTOM: #a6c2b2 1pt solid; PADDING-BOTTOM: 2px; TEXT-ALIGN: center; PADDING-TOP: 2px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #e1eae5; valign: bottom
}
TR.xdSubheadingRow {
	MIN-HEIGHT: 28px
}
TD.xdSubheadingCell {
	BORDER-BOTTOM: #a6c2b2 1pt solid; PADDING-BOTTOM: 4px; TEXT-ALIGN: center; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; valign: bottom
}
TR.xdHeadingRowEmphasis {
	MIN-HEIGHT: 27px
}
TD.xdHeadingCellEmphasis {
	BORDER-TOP: #a6c2b2 1pt solid; BORDER-BOTTOM: #a6c2b2 1pt solid; PADDING-BOTTOM: 2px; TEXT-ALIGN: center; PADDING-TOP: 2px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #e1eae5; valign: bottom
}
TR.xdSubheadingRowEmphasis {
	MIN-HEIGHT: 28px
}
TD.xdSubheadingCellEmphasis {
	BORDER-BOTTOM: #a6c2b2 1pt solid; PADDING-BOTTOM: 4px; TEXT-ALIGN: center; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 22px; valign: bottom
}
TR.xdTableLabelControlStackedRow {
	MIN-HEIGHT: 45px
}
TD.xdTableLabelControlStackedCellLabel {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px
}
TD.xdTableLabelControlStackedCellComponent {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px
}
TR.xdTableRow {
	MIN-HEIGHT: 30px
}
TD.xdTableCellLabel {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px
}
TD.xdTableCellComponent {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px
}
TD.xdTableMiddleCell {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px
}
TR.xdTableEmphasisRow {
	MIN-HEIGHT: 30px
}
TD.xdTableEmphasisCellLabel {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; BACKGROUND-COLOR: #c4d6cb
}
TD.xdTableEmphasisCellComponent {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #c4d6cb
}
TD.xdTableMiddleCellEmphasis {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; BACKGROUND-COLOR: #c4d6cb
}
TR.xdTableOffsetRow {
	MIN-HEIGHT: 30px
}
TD.xdTableOffsetCellLabel {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 22px; PADDING-RIGHT: 5px; BACKGROUND-COLOR: #c4d6cb
}
TD.xdTableOffsetCellComponent {
	PADDING-BOTTOM: 4px; PADDING-TOP: 4px; PADDING-LEFT: 5px; PADDING-RIGHT: 22px; BACKGROUND-COLOR: #c4d6cb
}
P {
	FONT-SIZE: 11pt; COLOR: #354d3f; MARGIN-TOP: 0px
}
H1 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 24pt; FONT-WEIGHT: normal; COLOR: #354d3f; MARGIN-TOP: 0px
}
H2 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 16pt; FONT-WEIGHT: bold; COLOR: #354d3f; MARGIN-TOP: 0px
}
H3 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 12pt; TEXT-TRANSFORM: uppercase; FONT-WEIGHT: normal; COLOR: #354d3f; MARGIN-TOP: 0px
}
H4 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 10pt; FONT-WEIGHT: normal; COLOR: #262626; FONT-STYLE: italic; MARGIN-TOP: 0px
}
H5 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 10pt; FONT-WEIGHT: bold; COLOR: #354d3f; FONT-STYLE: italic; MARGIN-TOP: 0px
}
H6 {
	MARGIN-BOTTOM: 0px; FONT-SIZE: 10pt; FONT-WEIGHT: normal; COLOR: #262626; MARGIN-TOP: 0px
}
BODY {
	COLOR: black
}
</style>
			</head>
			<body>
				<div>
					<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 600px" borderColor="buttontext" border="1">
						<colgroup>
							<col style="WIDTH: 150px"></col>
							<col style="WIDTH: 150px"></col>
							<col style="WIDTH: 150px"></col>
							<col style="WIDTH: 150px"></col>
						</colgroup>
						<tbody vAlign="top">
							<tr>
								<td style="BACKGROUND-COLOR: transparent">
									<div align="center">
										<font color="#ffffff" size="2" face="Calibri">
											<strong><input title="" class="langFont" style="BORDER-TOP: #808080 6pt; BORDER-RIGHT: #808080 6pt; BORDER-BOTTOM: #808080 6pt; FONT-WEIGHT: bold; COLOR: #ffffff; MARGIN: 1px; BORDER-LEFT: #808080 6pt; WIDTH: 100%; BACKGROUND-COLOR: #002060" type="button" value="Core Information" xd:CtrlId="btn_CoreInfoView" xd:xctname="Button" tabIndex="0"/>
											</strong>
										</font>
									</div>
								</td>
								<td>
									<div>
										<font size="2" face="Calibri"><input title="" class="langFont" style="BORDER-TOP: #808080 6pt; BORDER-RIGHT: #808080 6pt; BORDER-BOTTOM: #808080 6pt; FONT-WEIGHT: bold; COLOR: #ffffff; MARGIN: 1px; BORDER-LEFT: #808080 6pt; WIDTH: 100%; BACKGROUND-COLOR: #002060" type="button" value="Related Documents" xd:CtrlId="btn_RelatedDocumentView" xd:xctname="Button" tabIndex="0"/>
										</font>
									</div>
								</td>
								<td>
									<div>
										<font size="2" face="Calibri"><input title="" class="langFont" style="BORDER-TOP: #808080 6pt; BORDER-RIGHT: #808080 6pt; BORDER-BOTTOM: #808080 6pt; FONT-WEIGHT: bold; COLOR: #ffffff; MARGIN: 1px; BORDER-LEFT: #808080 6pt; WIDTH: 100%; BACKGROUND-COLOR: #0070c0" type="button" value="Related Notes" xd:CtrlId="btn_RelatedNotesView" xd:xctname="Button" tabIndex="0">
												<xsl:attribute name="style">BORDER-TOP: #808080 6pt; BORDER-RIGHT: #808080 6pt; BORDER-BOTTOM: #808080 6pt; FONT-WEIGHT: bold; COLOR: #ffffff; MARGIN: 1px; BORDER-LEFT: #808080 6pt; WIDTH: 100%; BACKGROUND-COLOR: #0070c0;<xsl:choose>
														<xsl:when test="1 = 1">caption: Disable</xsl:when>
													</xsl:choose>
												</xsl:attribute>
												<xsl:choose>
													<xsl:when test="1 = 1">
														<xsl:attribute name="disabled">true</xsl:attribute>
													</xsl:when>
												</xsl:choose>
											</input>
										</font>
									</div>
								</td>
								<td style="PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px; BORDER-LEFT-COLOR: ">
									<div>
										<font size="2" face="Calibri"><input title="" class="langFont" style="BORDER-TOP: #808080 6pt; BORDER-RIGHT: #808080 6pt; BORDER-BOTTOM: #808080 6pt; FONT-WEIGHT: bold; COLOR: #ffffff; MARGIN: 1px; BORDER-LEFT: #808080 6pt; WIDTH: 100%; BACKGROUND-COLOR: #002060" type="button" value="Upload Materials" xd:CtrlId="btn_MaterialsView" xd:xctname="Button" tabIndex="0"/>
										</font>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div>
					<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 700px" borderColor="buttontext" border="1">
						<colgroup>
							<col style="WIDTH: 700px"></col>
						</colgroup>
						<tbody vAlign="top">
							<tr>
								<td style="BORDER-TOP: #000000 1pt solid; BORDER-RIGHT: #000000 1pt solid; BORDER-BOTTOM: #000000 1pt solid; BORDER-LEFT: #000000 1pt solid; BACKGROUND-COLOR: #ffffff">
									<div style="FONT-WEIGHT: normal" align="right"> </div>
									<h1 style="FONT-WEIGHT: normal" align="center">Training Material Request</h1>
									<div style="FONT-WEIGHT: normal" align="right"> </div>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1pt solid; BORDER-RIGHT: #000000 1pt solid; BORDER-BOTTOM: #000000 1pt solid; BORDER-LEFT: #000000 1pt solid; BACKGROUND-COLOR: #f2f2f2">
									<div>
										<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 696px" borderColor="buttontext" border="1">
											<colgroup>
												<col style="WIDTH: 126px"></col>
												<col style="WIDTH: 570px"></col>
											</colgroup>
											<tbody vAlign="top">
												<tr>
													<td style="BORDER-RIGHT-COLOR: ">
														<div>
															<font size="2" face="Calibri">Lifecycle Status</font>
														</div>
													</td>
													<td style="BORDER-TOP-COLOR: ; BORDER-BOTTOM-COLOR: ; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 1px">
														<div>
															<font size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL3" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:FormInternal/my:LifecycleStatus" style="BORDER-TOP: #dcdcdc 1pt; BORDER-RIGHT: #dcdcdc 1pt; WHITE-SPACE: nowrap; BORDER-BOTTOM: #dcdcdc 1pt; TEXT-ALIGN: right; BORDER-LEFT: #dcdcdc 1pt; WIDTH: 100%; BACKGROUND-COLOR: transparent">
																	<xsl:value-of select="my:FormInternal/my:LifecycleStatus"/>
																</span>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td style="BORDER-TOP-COLOR: ; BORDER-BOTTOM-COLOR: ; BORDER-RIGHT-COLOR: ">
														<div>
															<font size="2" face="Calibri">Form Name</font>
														</div>
													</td>
													<td style="BORDER-TOP-COLOR: ; BORDER-BOTTOM-COLOR: ; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 1px">
														<div>
															<font size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL30" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:SystemInternal/my:FormName" style="BORDER-TOP: #dcdcdc 1pt; BORDER-RIGHT: #dcdcdc 1pt; WHITE-SPACE: nowrap; BORDER-BOTTOM: #dcdcdc 1pt; TEXT-ALIGN: right; BORDER-LEFT: #dcdcdc 1pt; WIDTH: 100%; BACKGROUND-COLOR: transparent">
																	<xsl:value-of select="my:SystemInternal/my:FormName"/>
																</span>
															</font>
														</div>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
									<div> </div>
									<div/>
									<div/>
									<div>
										<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 697px" borderColor="buttontext" border="1">
											<colgroup>
												<col style="WIDTH: 120px"></col>
												<col style="WIDTH: 577px"></col>
											</colgroup>
											<tbody vAlign="top">
												<tr>
													<td colSpan="2" style="BORDER-BOTTOM: #000000 1pt solid; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; PADDING-LEFT: 1px; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 1px; BACKGROUND-COLOR: #0070c0">
														<div align="center">
															<font color="#ffffff" size="4" face="Calibri">
																<strong>Core Information</strong>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td style="BORDER-TOP: #000000 1pt solid; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>
															<font size="2" face="Calibri">Request Title</font>
														</div>
													</td>
													<td style="BORDER-TOP: #000000 1pt solid; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>
															<font style="BACKGROUND-COLOR: #e9edf1" size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL1" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:Core/my:RequestTitle" xd:datafmt="&quot;string&quot;,&quot;plainMultiline&quot;" style="WORD-WRAP: break-word; WHITE-SPACE: normal; OVERFLOW-X: auto; OVERFLOW-Y: auto; WIDTH: 100%">
																	<xsl:choose>
																		<xsl:when test="function-available('xdFormatting:formatString')">
																			<xsl:value-of select="xdFormatting:formatString(my:Core/my:RequestTitle,&quot;string&quot;,&quot;plainMultiline&quot;)" disable-output-escaping="yes"/>
																		</xsl:when>
																		<xsl:otherwise>
																			<xsl:value-of select="my:Core/my:RequestTitle" disable-output-escaping="yes"/>
																		</xsl:otherwise>
																	</xsl:choose>
																</span>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td style="PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>Request Type</div>
													</td>
													<td style="PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>
															<font style="BACKGROUND-COLOR: #e9edf1" size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL13" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:Core/my:RequestType" style="WHITE-SPACE: nowrap; WIDTH: 342px">
																	<xsl:value-of select="my:Core/my:RequestType"/>
																</span>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td colSpan="2" style="BORDER-TOP-COLOR: ; PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 0px; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 0px">
														<div><xsl:apply-templates select="my:Core/my:PlannedTrainingSection" mode="_4"/>
														</div>
													</td>
												</tr>
												<tr>
													<td style="BORDER-TOP-COLOR: ; PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>Department</div>
													</td>
													<td style="BORDER-TOP-COLOR: ; PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>
															<font style="BACKGROUND-COLOR: #e9edf1" size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL25" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:Core/my:Department" style="WHITE-SPACE: nowrap; WIDTH: 342px">
																	<xsl:value-of select="my:Core/my:Department"/>
																</span>
															</font>
														</div>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
									<div> </div>
									<div>
										<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 697px" borderColor="buttontext" border="1">
											<colgroup>
												<col style="WIDTH: 110px"></col>
												<col style="WIDTH: 587px"></col>
											</colgroup>
											<tbody vAlign="top">
												<tr>
													<td colSpan="2" style="BORDER-BOTTOM: #000000 1pt solid; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; PADDING-LEFT: 1px; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 1px; BACKGROUND-COLOR: #0070c0">
														<div align="center">
															<font color="#ffffff" size="4" face="Calibri">
																<strong>Related Notes</strong>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td style="BORDER-TOP: #000000 1pt solid; PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 1px; PADDING-RIGHT: 1px">
														<div>
															<font size="2" face="Calibri">Notes</font>
														</div>
													</td>
													<td style="BORDER-TOP: #000000 1pt solid; BORDER-BOTTOM-COLOR: ">
														<div>
															<font size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" contentEditable="true" tabIndex="0" xd:CtrlId="CTRL22" xd:xctname="PlainText" xd:binding="my:Core/my:NotesSection/my:Notes" xd:datafmt="&quot;string&quot;,&quot;plainMultiline&quot;" style="WORD-WRAP: break-word; HEIGHT: 60px; WHITE-SPACE: normal; OVERFLOW-X: auto; OVERFLOW-Y: auto; WIDTH: 100%">
																	<xsl:choose>
																		<xsl:when test="function-available('xdFormatting:formatString')">
																			<xsl:value-of select="xdFormatting:formatString(my:Core/my:NotesSection/my:Notes,&quot;string&quot;,&quot;plainMultiline&quot;)" disable-output-escaping="yes"/>
																		</xsl:when>
																		<xsl:otherwise>
																			<xsl:value-of select="my:Core/my:NotesSection/my:Notes" disable-output-escaping="yes"/>
																		</xsl:otherwise>
																	</xsl:choose>
																</span>
															</font>
														</div>
													</td>
												</tr>
												<tr>
													<td colSpan="2" style="BORDER-TOP-COLOR: ; PADDING-BOTTOM: 5px; BORDER-BOTTOM-COLOR: ; PADDING-TOP: 5px; PADDING-LEFT: 0px; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 0px">
														<div><xsl:apply-templates select="my:Core/my:NotesSection" mode="_2"/>
														</div>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
									<div>
										<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 696px" borderColor="buttontext" border="1">
											<colgroup>
												<col style="WIDTH: 696px"></col>
											</colgroup>
											<tbody vAlign="top">
												<tr style="MIN-HEIGHT: 26px">
													<td style="BORDER-BOTTOM-COLOR: ; BORDER-RIGHT-COLOR: ; PADDING-RIGHT: 1px">
														<div align="right">
															<font size="2" face="Calibri"><input title="" class="langFont" style="MARGIN: 1px; WIDTH: 100px" type="button" value="Append Note" xd:CtrlId="btn_SaveNotes" xd:xctname="Button" tabIndex="0"/> <input title="" class="langFont" style="MARGIN: 1px; WIDTH: 100px" type="button" value="Close" xd:CtrlId="btn_Close" xd:xctname="Button" tabIndex="0"/>
															</font>
														</div>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</body>
		</html>
	</xsl:template>
	<xsl:template match="my:PlannedTrainingSection" mode="_4">
		<xsl:if test="not((not(contains(../my:RequestType, &quot;Training Request&quot;))))">
			<div title="" class="xdSection xdRepeating" style="BORDER-TOP: 0pt; BORDER-RIGHT: 0pt; BORDER-BOTTOM: 0pt; MARGIN: 0px; BORDER-LEFT: 0pt; WIDTH: 100%" align="left" xd:CtrlId="CTRL26" xd:xctname="Section" tabIndex="-1" xd:widgetIndex="0" xd:caption_0="Hide if not Training Request">
				<div>
					<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 694px" borderColor="buttontext" border="1">
						<colgroup>
							<col style="WIDTH: 120px"></col>
							<col style="WIDTH: 574px"></col>
						</colgroup>
						<tbody vAlign="top">
							<tr style="MIN-HEIGHT: 24px">
								<td>
									<div>
										<font size="2" face="Calibri">Planned Training</font>
									</div>
								</td>
								<td>
									<div>
										<font size="2" face="Calibri"><input title="" class="xdBehavior_Boolean" type="checkbox" value="" tabIndex="0" xd:CtrlId="CTRL27" xd:xctname="CheckBox" xd:binding="my:PlannedTraining" xd:boundProp="xd:value" xd:onValue="TRUE" xd:offValue="FALSE">
												<xsl:attribute name="style">
													<xsl:choose>
														<xsl:when test="1 = 1">caption: Disable</xsl:when>
													</xsl:choose>
												</xsl:attribute>
												<xsl:choose>
													<xsl:when test="1 = 1">
														<xsl:attribute name="disabled">true</xsl:attribute>
													</xsl:when>
												</xsl:choose>
												<xsl:attribute name="xd:value">
													<xsl:value-of select="my:PlannedTraining"/>
												</xsl:attribute>
												<xsl:if test="my:PlannedTraining=&quot;TRUE&quot;">
													<xsl:attribute name="CHECKED">CHECKED</xsl:attribute>
												</xsl:if>
											</input>
										</font>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</xsl:if>
	</xsl:template>
	<xsl:template match="my:NotesSection" mode="_2">
		<xsl:if test="not((my:NotesHistory = &quot;&quot;))">
			<div title="" class="xdSection xdRepeating" style="BORDER-TOP: 0pt; BORDER-RIGHT: 0pt; BORDER-BOTTOM: 0pt; MARGIN: 0px; BORDER-LEFT: 0pt; WIDTH: 100%" align="left" xd:CtrlId="CTRL21" xd:xctname="Section" tabIndex="-1" xd:widgetIndex="0" xd:caption_0="Hide if history is empty">
				<div>
					<table class="xdLayout" style="WORD-WRAP: break-word; BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-COLLAPSE: collapse; TABLE-LAYOUT: fixed; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 694px" borderColor="buttontext" border="1">
						<colgroup>
							<col style="WIDTH: 109px"></col>
							<col style="WIDTH: 585px"></col>
						</colgroup>
						<tbody vAlign="top">
							<tr>
								<td>
									<div>
										<font size="2" face="Calibri">History</font>
									</div>
								</td>
								<td>
									<div>
										<font size="2" face="Calibri"><span title="" class="xdTextBox" hideFocus="1" tabIndex="-1" xd:CtrlId="CTRL2" xd:xctname="PlainText" xd:disableEditing="yes" xd:binding="my:NotesHistory" xd:datafmt="&quot;string&quot;,&quot;plainMultiline&quot;" style="WORD-WRAP: break-word; WHITE-SPACE: normal; OVERFLOW-X: auto; OVERFLOW-Y: auto; WIDTH: 100%; BACKGROUND-COLOR: transparent">
												<xsl:choose>
													<xsl:when test="function-available('xdFormatting:formatString')">
														<xsl:value-of select="xdFormatting:formatString(my:NotesHistory,&quot;string&quot;,&quot;plainMultiline&quot;)" disable-output-escaping="yes"/>
													</xsl:when>
													<xsl:otherwise>
														<xsl:value-of select="my:NotesHistory" disable-output-escaping="yes"/>
													</xsl:otherwise>
												</xsl:choose>
											</span>
										</font>
									</div>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</xsl:if>
	</xsl:template>
</xsl:stylesheet>

<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:my="http://schemas.microsoft.com/office/infopath/2003/myXSD/2013-11-15T19:36:11" xmlns:xd="http://schemas.microsoft.com/office/infopath/2003" version="1.0">
	<xsl:output encoding="UTF-8" method="xml"/>
	<xsl:template match="/">
		<xsl:copy-of select="processing-instruction() | comment()"/>
		<xsl:choose>
			<xsl:when test="my:myFields">
				<xsl:apply-templates select="my:myFields" mode="_0"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="var">
					<xsl:element name="my:myFields"/>
				</xsl:variable>
				<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_0"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="my:PlannedTrainingSection" mode="_2">
		<xsl:copy>
			<xsl:element name="my:PlannedTraining">
				<xsl:choose>
					<xsl:when test="my:PlannedTraining">
						<xsl:copy-of select="my:PlannedTraining/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>FALSE</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:TrainingAttachmentGroup" mode="_5">
		<xsl:copy>
			<xsl:element name="my:TrainingAttachmentName">
				<xsl:copy-of select="my:TrainingAttachmentName/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:TrainingAttachmentURL">
				<xsl:copy-of select="my:TrainingAttachmentURL/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:TrainingAttachmentVersion">
				<xsl:copy-of select="my:TrainingAttachmentVersion/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:TrainingAttachmentRemove">
				<xsl:choose>
					<xsl:when test="my:TrainingAttachmentRemove">
						<xsl:copy-of select="my:TrainingAttachmentRemove/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>FALSE</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:TrainingAttachmentSection" mode="_4">
		<xsl:copy>
			<xsl:choose>
				<xsl:when test="my:TrainingAttachmentGroup">
					<xsl:apply-templates select="my:TrainingAttachmentGroup" mode="_5"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:TrainingAttachmentGroup"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_5"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:TrainingAttachments" mode="_3">
		<xsl:copy>
			<xsl:element name="my:TrainingAttachmentObj">
				<xsl:choose>
					<xsl:when test="my:TrainingAttachmentObj/text()[1]">
						<xsl:copy-of select="my:TrainingAttachmentObj/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:attribute name="xsi:nil">true</xsl:attribute>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:choose>
				<xsl:when test="my:TrainingAttachmentSection">
					<xsl:apply-templates select="my:TrainingAttachmentSection[1]" mode="_4"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:TrainingAttachmentSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_4"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:RelatedDocumentGroup" mode="_7">
		<xsl:copy>
			<xsl:element name="my:RelatedDocumentPath">
				<xsl:copy-of select="my:RelatedDocumentPath/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:RelatedDocumentHyperlink">
				<xsl:copy-of select="my:RelatedDocumentHyperlink/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:RelatedDocumentRemove">
				<xsl:choose>
					<xsl:when test="my:RelatedDocumentRemove">
						<xsl:copy-of select="my:RelatedDocumentRemove/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>FALSE</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:RelatedDocumentSection" mode="_6">
		<xsl:copy>
			<xsl:element name="my:RDPath">
				<xsl:copy-of select="my:RDPath/text()[1]"/>
			</xsl:element>
			<xsl:choose>
				<xsl:when test="my:RelatedDocumentGroup">
					<xsl:apply-templates select="my:RelatedDocumentGroup" mode="_7"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:RelatedDocumentGroup"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_7"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:NotesSection" mode="_8">
		<xsl:copy>
			<xsl:element name="my:Notes">
				<xsl:copy-of select="my:Notes/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:NotesHistory">
				<xsl:copy-of select="my:NotesHistory/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:Core" mode="_1">
		<xsl:copy>
			<xsl:element name="my:RequestTitle">
				<xsl:copy-of select="my:RequestTitle/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:RequestType">
				<xsl:copy-of select="my:RequestType/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:Department">
				<xsl:copy-of select="my:Department/text()[1]"/>
			</xsl:element>
			<xsl:choose>
				<xsl:when test="my:PlannedTrainingSection">
					<xsl:apply-templates select="my:PlannedTrainingSection[1]" mode="_2"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:PlannedTrainingSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_2"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:TrainingAttachments">
					<xsl:apply-templates select="my:TrainingAttachments[1]" mode="_3"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:TrainingAttachments"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_3"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:RelatedDocumentSection">
					<xsl:apply-templates select="my:RelatedDocumentSection[1]" mode="_6"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:RelatedDocumentSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_6"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:NotesSection">
					<xsl:apply-templates select="my:NotesSection[1]" mode="_8"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:NotesSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_8"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:SystemInternal" mode="_9">
		<xsl:copy>
			<xsl:element name="my:FormGUID">
				<xsl:copy-of select="my:FormGUID/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:FormName">
				<xsl:copy-of select="my:FormName/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:UserMessage">
				<xsl:copy-of select="my:UserMessage/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:PreviousView">
				<xsl:copy-of select="my:PreviousView/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:SaveLocation">
				<xsl:copy-of select="my:SaveLocation/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:HideReturnBtn">
				<xsl:choose>
					<xsl:when test="my:HideReturnBtn">
						<xsl:copy-of select="my:HideReturnBtn/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>FALSE</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="my:FormWFStageNum">
				<xsl:choose>
					<xsl:when test="my:FormWFStageNum">
						<xsl:copy-of select="my:FormWFStageNum/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>0</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="my:RemoveType">
				<xsl:copy-of select="my:RemoveType/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:FormInternal" mode="_10">
		<xsl:copy>
			<xsl:element name="my:LifecycleStatus">
				<xsl:choose>
					<xsl:when test="my:LifecycleStatus">
						<xsl:copy-of select="my:LifecycleStatus/text()[1]"/>
					</xsl:when>
					<xsl:otherwise>New</xsl:otherwise>
				</xsl:choose>
			</xsl:element>
			<xsl:element name="my:TaskOwnerId">
				<xsl:copy-of select="my:TaskOwnerId/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:Admin" mode="_11">
		<xsl:copy>
			<xsl:element name="my:IsTaskOwner">
				<xsl:copy-of select="my:IsTaskOwner/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:IsAdminGroup">
				<xsl:copy-of select="my:IsAdminGroup/text()[1]"/>
			</xsl:element>
			<xsl:element name="my:AdminGroup">
				<xsl:copy-of select="my:AdminGroup/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:RequestTypeGroup" mode="_14">
		<xsl:copy>
			<xsl:element name="my:RequestTypeValue">
				<xsl:copy-of select="my:RequestTypeValue/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:RequestTypeSection" mode="_13">
		<xsl:copy>
			<xsl:choose>
				<xsl:when test="my:RequestTypeGroup">
					<xsl:apply-templates select="my:RequestTypeGroup" mode="_14"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:RequestTypeGroup"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_14"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:DepartmentGroup" mode="_16">
		<xsl:copy>
			<xsl:element name="my:DepartmentValue">
				<xsl:copy-of select="my:DepartmentValue/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:DepartmentSection" mode="_15">
		<xsl:copy>
			<xsl:choose>
				<xsl:when test="my:DepartmentGroup">
					<xsl:apply-templates select="my:DepartmentGroup" mode="_16"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:DepartmentGroup"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_16"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:FieldSouce" mode="_12">
		<xsl:copy>
			<xsl:choose>
				<xsl:when test="my:RequestTypeSection">
					<xsl:apply-templates select="my:RequestTypeSection[1]" mode="_13"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:RequestTypeSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_13"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:DepartmentSection">
					<xsl:apply-templates select="my:DepartmentSection[1]" mode="_15"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:DepartmentSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_15"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:HaveYouSection" mode="_17">
		<xsl:copy>
			<xsl:element name="my:Haveyoumessage">
				<xsl:copy-of select="my:Haveyoumessage/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:Debug" mode="_18">
		<xsl:copy>
			<xsl:element name="my:DebugMessage">
				<xsl:copy-of select="my:DebugMessage/text()[1]"/>
			</xsl:element>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="my:myFields" mode="_0">
		<xsl:copy>
			<xsl:choose>
				<xsl:when test="my:Core">
					<xsl:apply-templates select="my:Core[1]" mode="_1"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:Core"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_1"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:SystemInternal">
					<xsl:apply-templates select="my:SystemInternal[1]" mode="_9"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:SystemInternal"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_9"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:FormInternal">
					<xsl:apply-templates select="my:FormInternal[1]" mode="_10"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:FormInternal"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_10"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:Admin">
					<xsl:apply-templates select="my:Admin[1]" mode="_11"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:Admin"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_11"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:FieldSouce">
					<xsl:apply-templates select="my:FieldSouce[1]" mode="_12"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:FieldSouce"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_12"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:HaveYouSection">
					<xsl:apply-templates select="my:HaveYouSection[1]" mode="_17"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:HaveYouSection"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_17"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:choose>
				<xsl:when test="my:Debug">
					<xsl:apply-templates select="my:Debug[1]" mode="_18"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:variable name="var">
						<xsl:element name="my:Debug"/>
					</xsl:variable>
					<xsl:apply-templates select="msxsl:node-set($var)/*" mode="_18"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>
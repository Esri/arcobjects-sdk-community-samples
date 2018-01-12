dojo.provide("arcgis.soe.NetRESTSOEProperties.NetSimpleRESTSOEWithProperties.Config");

dojo.require("dijit.form.ValidationTextBox");
dojo.require("dijit.form.CheckBox");
dojo.require("dijit.form.NumberSpinner");
dojo.require("dijit.form.Select");
dojo.require("dijit._Templated");

dojo.require("esri.discovery.dijit.services._CustomSoeConfigurationPane");

dojo.declare("arcgis.soe.NetRESTSOEProperties.NetSimpleRESTSOEWithProperties.Config", [esri.discovery.dijit.services._CustomSoeConfigurationPane, dijit._Templated], {

    templatePath: dojo.moduleUrl("arcgis.soe.NetRESTSOEProperties.NetSimpleRESTSOEWithProperties", "templates/NetSimpleRESTSOEWithProperties.html"),
  widgetsInTemplate: true,
  typeName: "NetSimpleRESTSOEWithProperties",  
  _capabilities: null,
  
  // some UI element references...
 _layerTypeTextBox: null,
  _maxNumFeaturesNumberSpinner: null,
  _returnFormatSelect: null,
  _isEditableCheckBox: null,
  
  _setProperties: function(extension) {
    this.inherited(arguments); // REQUIRED... we need this so capabilities are automatically handled
   this.set({
      layerType: extension.properties.layerType,
      maxNumFeatures: extension.properties.maxNumFeatures,
      returnFormat: extension.properties.returnFormat,
      isEditable: extension.properties.isEditable
    });
  },
  
  getProperties: function() {
    var myCustomSoeProps = {
      properties: {
        layerType: this.get("layerType"),
        maxNumFeatures: this.get("maxNumFeatures"),
        returnFormat: this.get("returnFormat"),
        isEditable: this.get("isEditable")
      }
    };
    
    // REQUIRED!!! This will overlay all your properties (capabilities) onto the default parent function return value
   return dojo.mixin(this.inherited(arguments), myCustomSoeProps);
  },
  
  // setters and getters... 
 // I use getters and setters in this example, but you could easily access the content of these widgets in the getProperties()
 // and _setProperties() functions respectively.  If you have some advanced business logic or validation to do, the getters/setters
 // can do the job.
 _setLayerTypeAttr: function(layerType) {
    this._layerTypeTextBox.set("value", layerType);
  },
  
  _setMaxNumFeaturesAttr: function(maxNumFeatures) {
    this._maxNumFeaturesNumberSpinner.set("value", maxNumFeatures);
  },
  
  _setReturnFormatAttr: function(returnFormat) {
    this._returnFormatSelect.set("value", returnFormat);
  },
  
  _setIsEditableAttr: function(isEditable) {
    this._isEditableCheckBox.set("checked", isEditable === true || isEditable === "true");
  },
  
  _getLayerTypeAttr: function() {
    return this._layerTypeTextBox.get("value");
  },
  
  _getMaxNumFeaturesAttr: function() {
    return this._maxNumFeaturesNumberSpinner.get("value");
  },
  
  _getReturnFormatAttr: function() {
    return this._returnFormatSelect.get("value");
  },
  
  _getIsEditableAttr: function() {
    return this._isEditableCheckBox.get("checked");
  }
});

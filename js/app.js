function EntriesModel() {
	this.EntryId = ko.observable();
	this.CreatedDate = ko.observable();
	this.Name = ko.observable();
	this.EntryContent = ko.observable();
}

function ViewModel() {
	var self = this;
	
	self.entries = new EntriesModel();
	self.addEntry = function () {

		$.ajax({
			type: 'POST',
			url: 'http://localhost:61144/api/entries',
			contentType: 'application/json;charset=utf-8',
			data: ko.mapping.toJSON(self.entries),
			success: function (data) {
				alert('added');

			}
		});
		

export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    // TODO add additional properties if needed
    this.coverImg = data.coverImg
    this.theme = data.theme || false
    this.twitter= data.twitter
    this.instagram = data.instagram
    this.facebook = data.facebook
  }
}

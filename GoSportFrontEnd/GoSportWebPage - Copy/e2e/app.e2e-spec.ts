import { GoSportWebPagePage } from './app.po';

describe('go-sport-web-page App', function() {
  let page: GoSportWebPagePage;

  beforeEach(() => {
    page = new GoSportWebPagePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

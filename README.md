weather-navigator
=================

Programmer test example.

notes
=====
1. Element ids were used where possible, but the rest is very dependent upon nesting position. This will make the code very susceptible to breaking if the page layout changes. We have no control over this problem, so the best we could hope to do is add error checking to make sure the items exist and they match some sort of regular expression pattern match.
1. The first page load could likely be skipped by navigating directly to "http://www.weather.com/weather/today/32701", but that did not seem to be in the spirit of the assignment.
1. Error checking and unit tests need to be added.
1. It might make sense to look for either an RSS or mobile version to keep page load time and processing down if this was going to be done in a backend layer that did not directly surface to a user.
1. I learned something new and had fun. Thanks for the challenge.

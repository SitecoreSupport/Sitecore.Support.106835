using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Aggregation.Pipelines.ContactProcessing;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Model.Entities;
using Sitecore.ContentSearch.Analytics.Pipelines.AggregationFilters;


namespace Sitecore.Support.ContentSearch.Analytics.Pipelines.AggregationFilters
{
    public class InboundContactIdentifierFilter : AggregationInboundFilterProcessor
    {

        protected override void DoProcess(AgregationFilterPipelineArgs<IVisitAggregationContext> args)
        {
            this.DoProcess(args, args.Context.Contact);
        }

        protected override void DoProcess(AgregationFilterPipelineArgs<ContactProcessingArgs> args)
        {
            if (args.Context.Reason != ProcessingReason.Obsoleted)
            {
                this.DoProcess(args, args.Context.GetContact());
            }
        }

        protected void DoProcess(AgregationFilterPipelineArgs args, IContact contact)
        {
            string identifier;
            if (contact != null)
            {
                identifier = contact.Identifiers.Identifier;
                args.IsExcluded = string.IsNullOrEmpty(identifier);
                if (args.IsExcluded)
                {
                    args.FilteredMessage = "Filtered by Identifier";
                }
            }
        }
    }
}